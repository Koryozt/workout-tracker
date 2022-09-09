using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Workout_Tracker_API.Context;
using Workout_Tracker_API.Models;
using Workout_Tracker_API.Services;

namespace Workout_Tracker_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Auth/[Controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILoginService _login;
        
        public LoginController(IConfiguration configuration, ILoginService login)
        {
            _configuration = configuration;
            _login = login;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLogin Login)
        {
            User? User = await _login.Authenticate(Login);

            if (User is not null)
            {
                string Token = _login.Generate(User);
                return Ok(Token);
            }

            return Unauthorized("You're not logged in, register in: https:/localhost:7282/Auth/Register");
        }
    }
}