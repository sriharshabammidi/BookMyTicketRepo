using BookMyTicket.Core;
using BookMyTicket.Core.ClientContext;
using BookMyTicket.Interfaces.Services;
using BookMyTicket.Models;
using BookMyTicket.Models.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;

namespace BookMyTicket.API.Filters
{
    public class EnsureUserLoggedIn : ActionFilterAttribute
    {
        private readonly IClientContext _clientContext;

        private readonly IHttpContextAccessor _httpContext;

        private readonly IUserService _userService;

        private readonly JwtIssuerOptions _jwtIssuerOptions;


        public EnsureUserLoggedIn()
        {
        }

        public EnsureUserLoggedIn(
            IClientContext userContext,
            IHttpContextAccessor httpContext,
            IUserService userService,
            JwtIssuerOptions jwtIssuerOptions)
        {
            _clientContext = userContext;
            _httpContext = httpContext;
            _userService = userService;
            _jwtIssuerOptions = jwtIssuerOptions;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string userId = _httpContext.HttpContext.User.FindFirst("UserId")?.Value;
            DateTime? tokenExpiry = null;
            try
            {
                tokenExpiry = Convert.ToDateTime(_httpContext.HttpContext.User.FindFirst("ExpiresOn")?.Value);
            }
            catch (Exception)
            {
                // throw;
            }

            string sessionId = _httpContext.HttpContext.User.FindFirst("SessionId")?.Value;

            if (String.IsNullOrEmpty(userId) && Int32.TryParse(userId, out int id))
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Result = new JsonResult(new ApiResponse<object>(HttpStatusCode.Unauthorized, null, new ApiResponseErrorResult(null, "Unauthorized")));
                return;
            }


            _clientContext.UserInfo = (UserProfile)_userService.GetUserById(Int32.Parse(userId));
            _clientContext.SessionId = sessionId;


            if (tokenExpiry != null)
            {
                _clientContext.TokenExpiry = tokenExpiry;
            }

        }

        public override void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            if (_clientContext.TokenExpiry != null)
            {
                DateTime tokenExpiresOn = Convert.ToDateTime(_clientContext.TokenExpiry);
                if (DateTime.Now.AddMinutes(15) > tokenExpiresOn)
                {
                    // Generate new token
                    try
                    {
                        if (string.IsNullOrEmpty(actionExecutedContext.HttpContext.Response.Headers["RefreshToken"]))
                        {
                            var token = JwtTokenHelper.GenerateJSONWebToken(
                                _jwtIssuerOptions,
                                _clientContext.UserInfo.ID,
                                _clientContext.SessionId);
                            actionExecutedContext.HttpContext.Response.Headers.Add("RefreshToken", token);
                        }
                    }
                    catch (Exception ex)
                    {
                        // throw;
                    }
                }
            }
        }
    }
}
