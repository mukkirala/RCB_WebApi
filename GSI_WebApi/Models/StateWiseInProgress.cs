using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NRL_WebApi.Models
{
    public class StateWiseInProgress : NoOFBlockAgencyWise
    {
        public string UNFCStage { get; set; }
        public string StateName { get; set; }
    }
}