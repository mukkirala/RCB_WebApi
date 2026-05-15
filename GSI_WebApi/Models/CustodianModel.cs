using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GSI_WebApi.Models
{
    public class CustodianModel
    {
        public string CustodianID { get; set; }
        public string CustodianDepartmentCode { get; set; }
        public string CustodianName { get; set; }
        public string Designation { get; set; }
        public string DepartmentName { get; set; }
    }
}