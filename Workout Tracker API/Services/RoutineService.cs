using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Workout_Tracker_API.Context;
using Workout_Tracker_API.Models;
using Workout_Tracker_API.Services.Interfaces;

namespace Workout_Tracker_API.Services
{
    public class RoutineService : IRoutineService
    {        
        private readonly WorkoutContext? _context;
        public static string URL = "https://localhost:7282/Workout/Exercise/GetExercise/";


        public RoutineService(WorkoutContext context)
        {
            _context = context;
        }

        public async Task<List<string>> FindLinks(List<string> ExerciseList)
        {
            List<string> UrlList = new List<string>();

            if (_context is null || _context.Exercises is null)
                return new List<string>();

            foreach(string str in ExerciseList)
            {
                Exercise? Ex = await _context.Exercises.FirstOrDefaultAsync(e => str.ToLower() == e.Name.ToLower());
                string? Link = Ex is not null ? URL + Ex.ID : null;

                if (Link is not null)
                    UrlList.Add(Link); 
            }

            return UrlList;
        }

        public async Task<List<string>> GetExerciseLink(IEnumerable<Routine> Routines)
        {
            List<string> ExercisesLinks = new();

            foreach(Routine rtn in Routines)
            {
                string NoWhiteSpaceRoutines = Utilities.RemoveWhiteSpacesAtStart(rtn.RoutineExercises);
                List<string> Splited = NoWhiteSpaceRoutines.Split(',').ToList(), Links = await FindLinks(Splited);

                string Link = string.Join(", ", Links);

                ExercisesLinks.Add(Link.TrimEnd(' '));
            }

            return ExercisesLinks;
        }
    }
}