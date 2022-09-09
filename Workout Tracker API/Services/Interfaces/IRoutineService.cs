using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workout_Tracker_API.Models;

namespace Workout_Tracker_API.Services.Interfaces
{
    public interface IRoutineService
    {
        Task<List<string>> GetExerciseLink(IEnumerable<Routine> Routines);
        Task<List<string>> FindLinks(List<string> ExerciseList);   
    }
}