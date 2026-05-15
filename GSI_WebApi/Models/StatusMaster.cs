using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NRL_WebApi.Models
{
    public class StatusMaster
    {
        public int StatusID { get; set; }
        public string StatusName { get; set; }
        public string StatusCode { get; set; }
        public string Status { get; set; }
        
    }
}