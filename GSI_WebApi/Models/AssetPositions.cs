using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GSI_WebApi.Models
{
    public class AssetPositions
    {
        public int AssetID { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
      
        public string rfidTag { get; set; }

        public int slno { get; set; }
    }
}