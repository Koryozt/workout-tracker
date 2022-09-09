using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Workout_Tracker_API.Models;
using Microsoft.AspNetCore.Mvc;
using Workout_Tracker_API.Context;
using Microsoft.EntityFrameworkCore.Internal;
using Workout_Tracker_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Cryptography;

namespace Workout_Tracker_API.Services
{
    public class RegisterService : Controller, IRegisterService
    {   
        private readonly WorkoutContext _context;
        
        public RegisterService(WorkoutContext context)
        {
            _context = context;            
        }

        public async Task<IActionResult> RegisterUser(User NewUser)
        {
            if (_context is null || _context.Users is null)
            {
                return BadRequest();
            }

            bool Registered = await IsRegistered(NewUser);

            if (Registered)
                return BadRequest("User already exists");
            
            await _context.Users.AddAsync(NewUser);
            await _context.SaveChangesAsync();

            return Ok("Registed succesfully");
        }

        public async Task<bool> IsRegistered(User NewUser)
        {
            if (_context.Users is null)
            {
                return true;
            }

            return await _context.Users.AnyAsync(ExUser => NewUser.Email == ExUser.Email || NewUser.Username == ExUser.Username);
        }



    }
}