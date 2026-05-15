using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NRLWebApi.Models
    {
    public class ScanQRHistoryModel
        {
        public int AuditDetailsID { get;set;}
        public string MainAssetNumber { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }
        public int AuditID { get; set; }
        public DateTime Date { get; set; }
    }
    }