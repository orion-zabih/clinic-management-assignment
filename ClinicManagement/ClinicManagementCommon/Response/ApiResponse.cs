using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementCommon.Response
{
    public class ApiResponse
    {
        public string Code { get; set; }
        public string Message { get; set; }

        public ApiResponse()
        {
            Code = Message = string.Empty;
        }
    }
}
