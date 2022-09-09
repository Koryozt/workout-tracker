using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Workout_Tracker_API.Context;
using Workout_Tracker_API.Models;

namespace Workout_Tracker_API.Services
{
    public class Utilities
    {
        private readonly WorkoutContext? _context;

        public Utilities(WorkoutContext Context)
        {
            _context = Context;
        }

        public bool Exists(int id)
        {
            if (_context is null || _context.Exercises is null)
                return false;
            
            return _context.Exercises.Any(e => e.ID == id);
        }

        public static string RemoveWhiteSpacesAtStart(string Exercises)
        {
            string[] Ex = Exercises.Split(' ');
            List<string> NoWS = new List<string>();

            foreach(string s in Ex)
            {
                string a = Regex.Replace(s, @"[^a-zA-Z0-9 ,]", "");
                NoWS.Add(a);
            }

            return string.Join("", NoWS);
        }
    }
}