using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NRL_WebApi.Models
{
    public class GetAgencyWiseBlockAreaDM : NoOFBlockAgencyWise
    {
        public string DM_Area_SQ_KM { get; set; }
    }
}