using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NRL_WebApi.Models
{
    public class UserRegistration
    {
        public int AuditDetailsID { get; set; }
        public string MainAssetNumber { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }
        public int AuditID { get; set; }
        public int AssetID { get; set; }
        public string CustodianID { get; set; }
        public DateTime Date { get; set; }
        public string AuditBy { get; set; }
        public string Location { get; set; }
        //public int UserID { get; set; }
        //public string Username { get; set; }

        //public string MobileNo { get; set; }
        //public string Password { get; set; }
        public int AssetClassID { get; set; }
    }
}