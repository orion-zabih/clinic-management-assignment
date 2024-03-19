using System;
using System.Collections.Generic;

namespace ClinicManagementDataAccess.Models;

public partial class Patient
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string? Address { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreateDateTime { get; set; }

    public DateTime? UpdateDateTime { get; set; }

    public DateTime DoB { get; set; }

    public virtual ICollection<Visit> Visit { get; set; } = new List<Visit>();
}
