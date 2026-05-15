using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GSI_WebApi.Models
{
    public class AssetAssignRequest
    {
        public int LocationId { get; set; }
        public long AssetId { get; set; }
        public string CustodianId { get; set; }
    }
}