using BookMyTicket.Models.Core;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookMyTicket.Core
{
    public static class JwtTokenHelper
    {
        public static string GenerateJSONWebToken(JwtIssuerOptions jwtIssuerOptions, long id, string sessionId = null)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("iNivDmHLpUA223sqsfhqGbMRdRj1PVkH"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenExpiry = DateTime.Now.AddMinutes(120);
            var _sessionId = sessionId;
            if (sessionId == null)
            {
                _sessionId = Guid.NewGuid().ToString();
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", id.ToString()),
                    new Claim("SessionId", _sessionId.ToString()),
                }),
                Expires = tokenExpiry,
                SigningCredentials = credentials,
                Issuer = jwtIssuerOptions.Issuer,
                Audience = jwtIssuerOptions.Audience,
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
