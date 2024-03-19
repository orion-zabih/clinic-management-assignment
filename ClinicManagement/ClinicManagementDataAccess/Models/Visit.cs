using System;
using System.Collections.Generic;

namespace ClinicManagementDataAccess.Models;

public partial class Visit
{
    public int Id { get; set; }

    public string? VisitDetail { get; set; }

    public int? DoctorId { get; set; }

    public DateTime? CreateDateTime { get; set; }

    public DateTime? UpdateDateTime { get; set; }

    public int? PatientId { get; set; }

    public virtual ICollection<Disease> Disease { get; set; } = new List<Disease>();

    public virtual Doctor? Doctor { get; set; }

    public virtual Patient? Patient { get; set; }
}
