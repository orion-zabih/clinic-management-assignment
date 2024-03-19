﻿using ClinicManagementCommon.Models;
using ClinicManagementCommon.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementCommon.ApiClient
{
    internal class InvPatientClient
    {
        string invSaleApiUrl = "/api/PatientManagement";
        public List<DoctorInfo> GetDoctorInfo()//,string prodLedger="false"
        {
            try
            {
                try
                {

                    invSaleApiUrl = "/api/PatientManagement" + "/GetDoctorInfo" ;
                    // var json = JsonConvert.SerializeObject(signinDTO);
                    var responses = ApiManager.GetAsync(invSaleApiUrl);

                    // List data response.
                    if (responses != null)
                    {
                        //using (var streamReader = new StreamReader(responses))
                        {
                            //var jsonResult = streamReader.ReadToEnd();
                            var response = JsonConvert.DeserializeObject<List<DoctorInfo>>(responses);
                            if (response != null)
                            {
                                return response;
                            }
                            return null;

                        }

                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<PatientReport> GetPatientInfo(SearchCriteria searchCriteria)//,string prodLedger="false"
        {
            try
            {
                try
                {

                    invSaleApiUrl = "/api/PatientManagement" + "/GetPatientInfo?Name="+searchCriteria.Name+"&gender="+searchCriteria.gender;
                    // var json = JsonConvert.SerializeObject(signinDTO);
                    var responses = ApiManager.GetAsync(invSaleApiUrl);

                    // List data response.
                    if (responses != null)
                    {
                        //using (var streamReader = new StreamReader(responses))
                        {
                            //var jsonResult = streamReader.ReadToEnd();
                            var response = JsonConvert.DeserializeObject<List<PatientReport>>(responses);
                            if (response != null)
                            {
                                return response;
                            }
                            return null;

                        }

                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public ApiResponse PostPatientManagement(PatientManagement dataResponse)
        {
            ApiResponse responseDTO = new ApiResponse();
            try
            {
                invSaleApiUrl = "/api/PatientManagement/PostPatientManagement";
                var json = JsonConvert.SerializeObject(dataResponse);
                var response = ApiManager.PostAsync(json, invSaleApiUrl);

                if (response != null)
                {
                    var result = JsonConvert.DeserializeObject<ApiResponse>(response);
                    if (result != null)
                        responseDTO = result;
                }

                return responseDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ApiResponse PostPatientManagementUpdate(PatientManagement dataResponse)
        {
            ApiResponse responseDTO = new ApiResponse();
            try
            {
                invSaleApiUrl = "/api/PatientManagement/PostPatientManagementUpdate";
                var json = JsonConvert.SerializeObject(dataResponse);
                var response = ApiManager.PostAsync(json, invSaleApiUrl);

                if (response != null)
                {
                    var result = JsonConvert.DeserializeObject<ApiResponse>(response);
                    if (result != null)
                        responseDTO = result;
                }

                return responseDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal PatientManagement GetPatientManagement(string patientId)
        {
            try
            {
                try
                {

                    invSaleApiUrl = "/api/PatientManagement" + "/GetPatientManagement?patientId="+ patientId;
                    // var json = JsonConvert.SerializeObject(signinDTO);
                    var responses = ApiManager.GetAsync(invSaleApiUrl);

                    // List data response.
                    if (responses != null)
                    {
                        //using (var streamReader = new StreamReader(responses))
                        {
                            //var jsonResult = streamReader.ReadToEnd();
                            var response = JsonConvert.DeserializeObject<PatientManagement>(responses);
                            if (response != null)
                            {
                                return response;
                            }
                            return null;

                        }

                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
