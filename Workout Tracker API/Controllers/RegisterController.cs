using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workout_Tracker_API.Models;
using Workout_Tracker_API.Services.Interfaces;

namespace Workout_Tracker_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Auth/[Controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterService _register;
        public RegisterController(IRegisterService Register)
        {
            _register = Register;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task Register([FromBody] User NewUser)
        {           
            await _register.RegisterUser(NewUser);
        }
    }
}