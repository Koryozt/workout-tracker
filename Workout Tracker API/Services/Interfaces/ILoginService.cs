using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workout_Tracker_API.Models;

namespace Workout_Tracker_API.Services
{
    public interface ILoginService
    {
        string Generate(User User);
        Task<User?> Authenticate(UserLogin Login);
    }
}