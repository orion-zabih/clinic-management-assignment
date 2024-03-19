using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementCommon.Models
{
    public  class DiseaseInfo
    {
        public int Id { get; set; }

        public int? VisitId { get; set; }
        [Required(ErrorMessage ="Disease name required")]
        public string DiseaseName { get; set; }
        public string? DiseaseDetail { get; set; }

    }
}
