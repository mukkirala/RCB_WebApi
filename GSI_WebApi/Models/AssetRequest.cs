using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GSI_WebApi.Models
{
    public class AssetRequest
    {
        public int AssetRequestID { get; set; }
        public string AssetTypeID { get; set; }
        public string AssetTypeCode { get; set; }
        public string AssetTypeName { get; set; }
        public string RequestBy { get; set; }
        public string EmployeeID { get; set; }
        public string Quantity { get; set; }
        public string Status { get; set; }
        public string Date { get; set; }
        public string CustodianDepartment { get; set; }
        public string CustDepartmentCode { get; set; }
        public string CustDesignation { get; set; }
        public string ApproverID { get; set; }
        public string Location { get; set; }

        public int AssetClassID { get; set; }
    }
}