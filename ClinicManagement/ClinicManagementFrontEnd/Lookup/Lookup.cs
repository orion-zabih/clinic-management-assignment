using ClinicManagementCommon.ApiClient;
using ClinicManagementCommon.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClinicManagementFrontEnd.Lookup
{
    public class Lookup:iLookup
    {
        public IEnumerable<SelectListItem> GetDoctors()
        {
            InvPatientClient invPatientClient = new InvPatientClient();
            List<DoctorInfo> doctorInfos = invPatientClient.GetDoctorInfo();
            if(doctorInfos!=null && doctorInfos.Count > 0)
            {

                IList<SelectListItem> items = new List<SelectListItem>();
                foreach(DoctorInfo doctorInfo in doctorInfos)
                {
                    items.Add(new SelectListItem { Text = doctorInfo.Name, Value = doctorInfo.Id.ToString() });
                }
                return items;
            }
            else
            {
                return Enumerable.Empty<SelectListItem>();
            }
        }
        public IEnumerable<SelectListItem> GetGenders()
        {
            IList<SelectListItem> items = new List<SelectListItem>
            {
                new SelectListItem{Text = "Male", Value = "m"},
                new SelectListItem{Text = "Female", Value = "f"},
                new SelectListItem{Text = "Other", Value = "o"}
            };
            return items;
        }
    }
}
