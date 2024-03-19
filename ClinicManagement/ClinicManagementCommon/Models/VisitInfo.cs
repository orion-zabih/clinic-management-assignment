using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementCommon.Models
{
    public class VisitInfo
    {
        public int Id { get; set; }
        
        public string? VisitDetail { get; set; }
        [Required(ErrorMessage = "Please select doctor")]
        public int? DoctorId { get; set; }

        public int? PatientId { get; set; }

        public string? CreateDateTime { get; set; }

        public VisitInfo()
        {
        }
    }
}
