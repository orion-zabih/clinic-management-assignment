using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementCommon.Models
{
    public class PatientInfo
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; } 

        public int Age { get; set; }
        [Required(ErrorMessage = "Date of birth is required")]
        public string? Dob { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; } = null!;
        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; } = null!;

        public string? Address { get; set; }

        public string? CreateDateTime { get; set; }
        public PatientInfo()
        {
        }
    }
    public class PatientReport
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int Age { get; set; }
        public string? Dob { get; set; }
        public string Gender { get; set; } = null!;
        public string Phone { get; set; } = null!;

        public string? Address { get; set; }

        public string Disease { get; set; } = null!;

        public string Doctor { get; set; } = null!;
        public string? CreateDateTime { get; set; }
        public PatientReport()
        {
        }
    }

}
