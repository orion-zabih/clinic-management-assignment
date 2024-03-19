using System;
using System.Collections.Generic;

namespace ClinicManagementDataAccess.Models;

public partial class Disease
{
    public int Id { get; set; }

    public int? VisitId { get; set; }

    public string? DiseaseDetail { get; set; }

    public DateTime? CreateDateTime { get; set; }

    public DateTime? UpdateDateTime { get; set; }

    public string DiseaseName { get; set; } = null!;

    public virtual Visit? Visit { get; set; }
}
