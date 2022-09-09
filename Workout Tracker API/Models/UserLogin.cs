using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Workout_Tracker_API.Models
{
    public class UserLogin
    {
        [Key]
        public int ID { get; set; }
 
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set;} = string.Empty;
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set;} = string.Empty;
    }
}