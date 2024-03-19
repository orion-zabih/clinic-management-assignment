using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClinicManagementFrontEnd.Lookup
{
    public interface iLookup
    {
        IEnumerable<SelectListItem> GetDoctors();
        IEnumerable<SelectListItem> GetGenders();
    }
}
