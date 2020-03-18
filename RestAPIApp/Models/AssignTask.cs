using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPIApp.Models
{
    public class AssignTask
    {
        [Key]
        public int TaskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual User AssignTo { get; set; }
        //public virtual User Assignee { get; set; }
    }

    public class AssignTaskVM
    {
        public int TaskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UserId { get; set; }
        public string AssignedUserName { get; set; }
    }
}
