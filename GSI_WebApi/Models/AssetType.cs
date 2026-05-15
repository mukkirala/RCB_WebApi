using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GSI_WebApi.Models
{
    public class AssetType
    {
        public int AssetTypeID { get; set; }
        public string AssetTypeName { get; set; }
        public string AssetTypeCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }
        public string AssetClassName { get; set; }
    }
}