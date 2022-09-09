using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workout_Tracker_API.Models;

namespace Workout_Tracker_API.Controllers
{
    [ApiController]
    [Route("[Controller]/[Action]")]
    public class UserController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public IActionResult Profile()
        {
            User? User = GetCurrentUser();

            if (User is null)
            {
                return NotFound("The user does not exist");
            }

            return Ok(User);
        }

        private User? GetCurrentUser()
        {
            ClaimsIdentity? Identity = HttpContext.User.Identity as ClaimsIdentity;

            if (Identity is null)
            {
                return null;
            }

            IEnumerable<Claim> IdentityClaims = Identity.Claims;

            return new User
            {
                Username = IdentityClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value ?? string.Empty,
                Email = IdentityClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value ?? string.Empty,
                SavedRoutines = IdentityClaims.FirstOrDefault(o => o.Type == ClaimTypes.UserData)?.Value ?? string.Empty
            };
        }
    }
}