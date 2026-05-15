using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GSI_WebApi.Models
{
    public class SaveAuditRequest
    {
        public int AuditID { get; set; }
        public List<string> RfidTags { get; set; }
        public string custodianID { get; set; }
    }
}