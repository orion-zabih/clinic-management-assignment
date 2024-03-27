﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementCommon.Models
{
    public class PatientManagement
    {
        public PatientInfo PatientInfo { get; set; }
        public VisitInfo visitInfo { get; set; }
        public DiseaseInfo DiseaseInfo { get; set; }
        public PatientManagement()        {

            PatientInfo = new PatientInfo();
                visitInfo=new VisitInfo();
            DiseaseInfo = new DiseaseInfo();
        }

    }
    public class PatientVisit
    {
        public int PatientId { get; set; }
        public List<VisitInfo> visitInfos { get; set; }
        public PatientVisit()
        {


            visitInfos = new List<VisitInfo>(); ;
            
        }

    }
    public class SearchCriteria
    {
        public string Name { get; set; }
        public string age { get; set; }
        public string gender { get; set; }
        public string disease { get; set; }
    }
}
