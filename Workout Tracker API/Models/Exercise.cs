using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Workout_Tracker_API.Models
{
    public class Exercise
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        [DisplayName("Exercise Type")]
        public string ExerciseType { get; set; } = string.Empty;

        [Required]
        public int Repetitions { get; set; }
        
        [Required]
        public int Sets { get; set; }
        
        [Required]
        public double? Weight { get; set; }

    }
}