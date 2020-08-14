using BookMyTicket.Interfaces.Services;
using BookMyTicket.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookMyTicket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("Signup")]
        public bool Post(UserProfile userProfile)
        {
            return _userService.AddUser(userProfile);
        }

        [HttpPost]
        [Route("Signin")]
        public UserProfile Login(LoginRequest loginRequest)
        {
            return _userService.Login(loginRequest.Email, loginRequest.Password);
        }
    }
}
