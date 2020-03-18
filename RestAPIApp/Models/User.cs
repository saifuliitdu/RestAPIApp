using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPIApp.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        //[Required]
        public string Email { get; set; }
        //[Required]
        public string Password { get; set; }
        //[Required]
        public string RetypePassword { get; set; }
        //[Required]
        public string Address { get; set; }
        //[Required]
        public string Mobile { get; set; }
        //[InverseProperty("AssignTo")]
        public virtual ICollection<AssignTask> AssignTasks { get; set; }
        //[InverseProperty("Assignee")]
        //public virtual ICollection<AssignTask> TaskAssignees { get; set; }
    }

    public class UserVM
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RetypePassword { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string Token { get; set; }
    }
}
