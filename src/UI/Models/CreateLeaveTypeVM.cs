using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Models
{
    public class CreateLeaveTypeVM
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Default Number of Days")]
        public int DefaultDay { get; set; }
    }
}