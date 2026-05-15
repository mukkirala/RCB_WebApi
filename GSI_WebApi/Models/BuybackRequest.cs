using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GSI_WebApi.Models
{
    public class BuybackRequest
    {
        public int AssetID { get; set; }
        public string EmployeeID { get; set; }
        public string Comments { get; set; }
        public string Status { get; set; }
        public string Date { get; set; }
        public string RequestBy { get; set; }
        public string CustodianDepartment { get; set; }
        public string CustDepartmentCode { get; set; }
        public string CustDesignation { get; set; }
        public string Location { get; set; }
    }
}