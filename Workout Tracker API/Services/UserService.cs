using Microsoft.EntityFrameworkCore;
using Workout_Tracker_API.Context;
using Workout_Tracker_API.Models;
using Workout_Tracker_API.Services.Interfaces;

namespace Workout_Tracker_API.Services
{
    public class UserService : IUserService
    {
        private readonly WorkoutContext? _context;
        public static string URL = "https://localhost:7282/Workout/Routine/GetRoutine/";

        public UserService(WorkoutContext context)
        {
            _context = context;
        }
        public async Task<List<string>> FindLinks(List<string> ExerciseList)
        {
            List<string> UrlList = new List<string>();

            if (_context is null || _context.Users is null)
                return new List<string>();

            foreach(string str in ExerciseList)
            {
                User? User = await _context.Users.FirstOrDefaultAsync(u => str.ToLower() == u.SavedRoutines.ToLower());
                string? Link = User is not null ? URL + User.ID : null;

                if (Link is not null)
                    UrlList.Add(Link); 
            }

            return UrlList;
        }

        public async Task<List<string>> GetExerciseLink(IEnumerable<User> Users)
        {
            List<string> ExercisesLinks = new();

            foreach(User Us in Users)
            {
                string NoWhiteSpaceRoutines = Utilities.RemoveWhiteSpacesAtStart(Us.SavedRoutines);
                List<string> Splited = NoWhiteSpaceRoutines.Split(',').ToList(), Links = await FindLinks(Splited);

                string Link = string.Join(", ", Links);

                ExercisesLinks.Add(Link.TrimEnd(' '));
            }

            return ExercisesLinks;
        }
    }
}