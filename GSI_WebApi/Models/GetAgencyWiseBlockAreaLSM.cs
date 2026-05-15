using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NRL_WebApi.Models
{
    public class GetAgencyWiseBlockAreaLSM : NoOFBlockAgencyWise
    {
        public string LSM_Area_SQ_KM { get; set; }
    }
}