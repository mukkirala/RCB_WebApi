using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NRL_WebApi.Models
{
    public class LocationMaster
    {
        public int LocationID { get; set; }
        public string Location { get; set; }
        public string LocationCode { get; set; }


        public string Block { get; set; }
        public string Status { get; set; }
    }
}