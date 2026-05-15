using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GSI_WebApi.Models
{
    public class AssetClassMasterModel
    {
        public int AssetClassID { get; set; }
        public string AssetClassName { get; set; }
        public string AssetClassCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
    }
}