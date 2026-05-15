using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NRL_WebApi.Models
{
    public class AgencyWiseBlock : NoOFBlockAgencyWise
    {
        public string UNFCStage { get; set; }
        public string Agency { get; set; }
    }
}