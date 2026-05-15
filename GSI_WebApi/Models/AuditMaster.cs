using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NRL_WebApi.Models
{
    public class AuditMaster
    {
        public int AuditID { get; set; }
        public string AuditDate { get; set; }
        public string AuditName { get; set; }
        public string AuditDescription { get; set; }
        public string AuditBy { get; set; }
        public string Status { get; set; }
        public int LocationID { get; set; }
        public string Location { get; set; }
        public string LocationCode { get; set; }
        public string CustodianDepartment { get; set; }
    }
}