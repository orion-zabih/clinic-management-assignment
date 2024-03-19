using ClinicManagementAPI.Classes;
using ClinicManagementCommon.Models;
using ClinicManagementCommon.Response;
using ClinicManagementDataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace ClinicManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientManagementController : Controller
    {
        
        [Route("PostPatientManagement")]
        [HttpPost]
        public IActionResult PostPatientManagement([FromBody] PatientManagement model)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                using (Entities dbContext = new Entities())
                {
                    using (var transaction = dbContext.Database.BeginTransaction())
                    {
                        Patient dbPatient = new Patient { 
                            Name= model.PatientInfo.Name,
                            DoB=Utility.GetDateTimeFromString(model.PatientInfo.Dob),
                            Gender=model.PatientInfo.Gender,
                            Phone=model.PatientInfo.Phone,
                            Address=model.PatientInfo.Address,
                            IsActive=true
                        };

                        Visit dbVisit =new Visit
                            {
                                VisitDetail = model.visitInfo.VisitDetail,
                                Patient = dbPatient,
                                DoctorId = model.visitInfo.DoctorId,
                                CreateDateTime = !string.IsNullOrEmpty(model.visitInfo.CreateDateTime) ? Utility.GetDateTimeFromString(model.visitInfo.CreateDateTime) : DateTime.Now
                            };
                            dbVisit.Disease.Add(new Disease
                            {
                                DiseaseDetail = model.DiseaseInfo.DiseaseDetail,
                                DiseaseName = model.DiseaseInfo.DiseaseName,
                                Visit = dbVisit,
                                CreateDateTime = DateTime.Now
                            });
                        dbPatient.Visit.Add(dbVisit);
                        dbContext.Add(dbPatient);

                        try
                        {
                            dbContext.SaveChanges();
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();

                            throw ex;
                        }
                    }
                    // dbContext.InvSaleMaster.AddRan
                }

                apiResponse.Code = ApplicationResponse.SUCCESS_CODE;
                apiResponse.Message = ApplicationResponse.SUCCESS_MESSAGE;
                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                apiResponse.Code = ApplicationResponse.GENERIC_ERROR_CODE;
                apiResponse.Message = ex.Message;
                return BadRequest(apiResponse);
            }

        }
        [Route("PostPatientManagement")]
        [HttpPost]
        public IActionResult PostPatientManagementUpdate([FromBody] PatientManagement model)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                using (Entities dbContext = new Entities())
                {
                    using (var transaction = dbContext.Database.BeginTransaction())
                    {
                        Patient dbPatient= dbContext.Patient.Include(p => p.Visit).ThenInclude(t => t.Disease).FirstOrDefault(g => g.IsActive == true && g.Id == model.PatientInfo.Id);
                        
                            dbPatient.Name = model.PatientInfo.Name;
                            dbPatient.DoB = Utility.GetDateTimeFromString(model.PatientInfo.Dob);
                            dbPatient.Gender = model.PatientInfo.Gender;
                            dbPatient.Phone = model.PatientInfo.Phone;
                            dbPatient.Address = model.PatientInfo.Address;
                        dbPatient.IsActive = true;
                        Visit dbVisit = dbPatient.Visit.FirstOrDefault();

                        dbVisit.VisitDetail = model.visitInfo.VisitDetail;
                        dbVisit.Patient = dbPatient;
                        dbVisit.DoctorId = model.visitInfo.DoctorId;
                        dbVisit.CreateDateTime = !string.IsNullOrEmpty(model.visitInfo.CreateDateTime) ? Utility.GetDateTimeFromString(model.visitInfo.CreateDateTime) : DateTime.Now;
                        
                        Disease dbDisease = dbVisit.Disease.FirstOrDefault();

                        dbDisease.DiseaseDetail = model.DiseaseInfo.DiseaseDetail;
                        dbDisease.DiseaseName = model.DiseaseInfo.DiseaseName;
                        dbDisease.Visit = dbVisit;
                        dbDisease.CreateDateTime = DateTime.Now;
                        dbPatient.Visit.Add(dbVisit);
                        dbContext.Add(dbPatient);

                        try
                        {
                            dbContext.SaveChanges();
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();

                            throw ex;
                        }
                    }
                    // dbContext.InvSaleMaster.AddRan
                }

                apiResponse.Code = ApplicationResponse.SUCCESS_CODE;
                apiResponse.Message = ApplicationResponse.SUCCESS_MESSAGE;
                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                apiResponse.Code = ApplicationResponse.GENERIC_ERROR_CODE;
                apiResponse.Message = ex.Message;
                return BadRequest(apiResponse);
            }

        }
        [Route("GetDoctorInfo")]
        [HttpGet]
        public IActionResult GetDoctorInfo()
        {
            List<DoctorInfo> doctorInfos = new List<DoctorInfo>();
            using (Entities context = new Entities())
            {
                var doctors = context.Doctor.Where(g => g.IsActive == true);
                if (doctors != null)
                {
                    doctorInfos = doctors.Select(s => new DoctorInfo { Id = s.Id, Name = s.Name }).ToList();
                }
            }
            if (doctorInfos != null && doctorInfos.Count > 0)
                return Ok(doctorInfos);
            else
                return NotFound();
        }
        [Route("GetPatientInfo")]
        [HttpGet]
        public IActionResult GetPatientInfo(string? Name,string? gender)//[FromBody] SearchCriteria searchCriteria
        {
            List<PatientReport> patientInfos = new List<PatientReport>();
            using (Entities context = new Entities())
            {
                var patients = context.Patient.Include(p=>p.Visit).ThenInclude(t=>t.Disease).Where(g => g.IsActive == true);
                if (!string.IsNullOrEmpty(Name))
                {
                    Name = Name.ToLower();
                    patients=patients.Where(p => p.Name.ToLower().Contains(Name));
                }
                if (!string.IsNullOrEmpty(gender))
                {
                    gender = gender.ToLower();
                    patients = patients.Where(p => p.Gender==gender);
                }
                //if (searchCriteria.age!=0)
                //{

                //}
                //if (!string.IsNullOrEmpty(searchCriteria.disease))
                //{
                //    searchCriteria.disease = searchCriteria.disease.ToLower();
                //    patients = patients.Where(p => p.Visit!=null && p.Visit.dis != null.ToLower().Contains(searchCriteria.Name));
                //}
                if (patients != null)
                {
                    patientInfos = patients.Select(s => new PatientReport { Id = s.Id, Name = s.Name,Dob=s.DoB.ToString() }).ToList();
                }
            }
            if (patientInfos != null && patientInfos.Count > 0)
                return Ok(patientInfos);
            else
                return NotFound();
        }
        [Route("GetPatientManagement")]
        [HttpGet]
        public IActionResult GetPatientManagement(string patientId)
        {
            PatientManagement patientManagement = new PatientManagement();
            using (Entities context = new Entities())
            {
                int p=int.Parse(patientId); 
                var patients = context.Patient.Include(p => p.Visit).ThenInclude(t => t.Disease).FirstOrDefault(g => g.IsActive == true && g.Id== p);
                if (patients != null)
                {
                    patientManagement.PatientInfo = new PatientInfo { 
                        Id=patients.Id,
                        Name = patients.Name,
                        Dob=patients.DoB.ToString(),
                        Phone=patients.Phone.ToString(),    
                        Address=patients.Address,
                        Gender=patients.Gender,
                        
                    };
                    var visit = patients.Visit.FirstOrDefault();
                    if (visit != null) {
                        patientManagement.visitInfo = new VisitInfo { Id = visit.Id,DoctorId=visit.DoctorId, VisitDetail = visit.VisitDetail };
                        var d = visit.Disease.FirstOrDefault();
                        if (d != null)
                        {
                            patientManagement.DiseaseInfo=new DiseaseInfo { Id = d.Id,DiseaseName=d.DiseaseName, DiseaseDetail = d.DiseaseDetail  };
                        }
                    }
                    
                }
            }
            if (patientManagement != null)
                return Ok(patientManagement);
            else
                return NotFound();
        }
    }
}
