using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Workout_Tracker_API.Models;

namespace Workout_Tracker_API.Services.Interfaces
{
    public interface IRegisterService
    {
        Task<IActionResult> RegisterUser(User NewUser);
        Task<bool> IsRegistered(User NewUser);
    }
}