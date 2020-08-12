using System.Collections.Generic;
using BookMyTicket.Interfaces.Services;
using BookMyTicket.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public UserProfile Get(long id)
        {
            return _userService.GetUserById(id);
        }

        // POST api/<UserController>
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
            return _userService.Login(loginRequest.UserName, loginRequest.Password);
        }
        
        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
