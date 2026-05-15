using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GSI_WebApi.Models
{
    public class InsertAuditData
    {
        // public int? AuditDetailsID { get; set; }
        // public string MainAssetNumber { get; set; }
        // public string Status { get; set; }
        // public string Comments { get; set; }
        // public int AuditID { get; set; }
        //public DateTime Date { get; set; }
        public int UserID { get; set; }
        public string Username { get; set; }
        public string MobileNo { get; set; }
        public string Password { get; set; }
    }
}