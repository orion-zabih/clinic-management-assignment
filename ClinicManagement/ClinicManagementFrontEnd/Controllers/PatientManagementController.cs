using ClinicManagementCommon.ApiClient;
using ClinicManagementCommon.Models;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementFrontEnd.Controllers
{
    public class PatientManagementController : Controller
    {
     
        [HttpGet]
        public IActionResult Create()
        {
            PatientManagement model = new PatientManagement();
            return View(model);
        }
        [HttpPost]
        public IActionResult Create(PatientManagement model)
        {

            InvPatientClient invPatientClient = new InvPatientClient();
            invPatientClient.PostPatientManagement(model);
            return View();
        }
        [HttpGet]
        public IActionResult Update(string PatientId)
        {
            PatientManagement model = new PatientManagement();
            InvPatientClient invPatientClient = new InvPatientClient();
            model=invPatientClient.GetPatientManagement(PatientId);
            return View(model);
        }
        [HttpPost]
        public IActionResult Update(PatientManagement model)
        {

            InvPatientClient invPatientClient = new InvPatientClient();
            invPatientClient.PostPatientManagementUpdate(model);
            return View();
        }
        [HttpGet]
        public IActionResult PatientReport()
        {
            SearchCriteria model = new SearchCriteria();
            return View(model);
        }
        [HttpPost]
        public IActionResult GetPatientReport(IDataTablesRequest request)
        {
            var Name = GetAdditionalParameterValue(request, "Name");
            var gender = GetAdditionalParameterValue(request, "gender");
            InvPatientClient invPatientClient = new InvPatientClient();
            var a =invPatientClient.GetPatientInfo(new SearchCriteria { Name = Name, gender = gender });
            var response = DataTablesResponse.Create(request, a.Count(), a.Count(), a);
            var dtjson = new DataTablesJsonResult(response);
            return dtjson;

        }
        public string GetAdditionalParameterValue(IDataTablesRequest request, string paramName)
        {
            if (request.AdditionalParameters.ContainsKey(paramName))
            {
                var JsonData = request.AdditionalParameters[paramName].ToString();
                var value = request.AdditionalParameters.ContainsKey(paramName) && JsonData != null ? JsonData.ToString() : string.Empty;
                return value;
            }
            else
            {
                return string.Empty;
            }

        }
        //[HttpGet]
        //public IActionResult VisitInfo()
        //{
        //    return PartialView(visitInfoView, new VisitInfo()); // Assuming _VisitInfo.cshtml is your partial view for VisitInfo
        //}
        //[HttpGet]
        //public IActionResult DiseaseInfo(IFormCollection obj)
        //{
        //    PatientManagement model = new PatientManagement();
        //    model.DiseaseInfo.Add(new DiseaseInfo());
        //    return View(diseaseInfoView, model);
        //}
        //public IActionResult DiseaseInfo()
        //{
        //    return PartialView(diseaseInfoView, new DiseaseInfo()); // Assuming _VisitInfo.cshtml is your partial view for VisitInfo
        //}
    }
}
