using System;
using System.Collections.Generic;

namespace ClinicManagementDataAccess.Models;

public partial class Doctor
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreateDateTime { get; set; }

    public DateTime? UpdateDateTime { get; set; }

    public virtual ICollection<Visit> Visit { get; set; } = new List<Visit>();
}
