using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Workout_Tracker_API.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        
        [Required]
        [DataType(DataType.Text)]       
        [StringLength(12)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [DataType(DataType.MultilineText)]
        public string SavedRoutines { get; set;} = string.Empty;
    
    }
}