using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Workout_Tracker_API.Models
{
    public class Routine
    {
        [Key]
        public int ID { get; set; }
        
        [StringLength(25)]
        [Required]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string Description { get; set; } = string.Empty;
        
        [Required]
        [DisplayName("Routine Type")]
        [StringLength(30)]
        public string RoutineType { get; set; } = string.Empty;
        
        [Required]
        [DisplayName("Routine's Exercises")]
        public string RoutineExercises { get; set; } = string.Empty;
        
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartingDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime FinishingDate { get; set; }

    }
}