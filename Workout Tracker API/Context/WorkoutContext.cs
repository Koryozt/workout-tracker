using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Workout_Tracker_API.Models;

namespace Workout_Tracker_API.Context
{
    public class WorkoutContext : DbContext
    {
        public WorkoutContext(DbContextOptions<WorkoutContext> Options) : base(Options)
        {
            
        }

        public DbSet<User>? Users { get; set; }
        public DbSet<UserLogin>? Logins { get; set; }
        public DbSet<Exercise>? Exercises { get; set; }
        public DbSet<Routine>? Routines { get; set; }
    }
}