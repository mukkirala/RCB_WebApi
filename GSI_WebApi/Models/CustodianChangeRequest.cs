using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GSI_WebApi.Models
{
    public class CustodianChangeRequest
    {
        public int CustodianChangeID { get; set; }
        public string AssetID { get; set; }
        public string EmployeeID { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
        public string CustodianComments { get; set; }
        public string ApproverComments { get; set; }
        public string ToCustodian { get; set; }
        public string TransferTo { get; set; }
        
        public string AdminComments { get; set; }
        public string RequestBy { get; set; }
        public string CustodianDepartment { get; set; }
        public string CustDepartmentCode { get; set; }
        public string CustDesignation { get; set; }
        public string RequestedChangeCustodian { get; set; }
        public string ApproverID { get; set; }
    }
}