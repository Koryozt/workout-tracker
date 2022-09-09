using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Workout_Tracker_API.Context;
using Workout_Tracker_API.Models;

namespace Workout_Tracker_API.Services
{
    public class LoginService : ILoginService
    {
        private readonly WorkoutContext _context;
        private readonly IConfiguration _config;
        public LoginService(WorkoutContext context, IConfiguration config)
        {
            _context = context;            
            _config = config;
        }
        public async Task<User?> Authenticate(UserLogin Login)
        {
            if (_context.Users is null)
                return null;

            User? CurrentUser = await _context.Users.FirstOrDefaultAsync(User =>
                                        Login.Email == User.Email && 
                                        Login.Password == User.Password);

            return CurrentUser ?? null;
        }

        public string Generate(User User)
        {
            SymmetricSecurityKey SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            SigningCredentials Credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);

            Claim[] Claims = new[]
            {
                new Claim(ClaimTypes.Email, User.Email),
                new Claim(ClaimTypes.NameIdentifier, User.Username),
                new Claim(ClaimTypes.UserData, User.SavedRoutines)
            };

            JwtSecurityToken Token = new JwtSecurityToken
            (
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims: Claims, 
                expires: DateTime.Now.AddHours(1),
                signingCredentials: Credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}