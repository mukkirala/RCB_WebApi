using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GSI_WebApi.Models
{
    public class AllocatedAssets
    {
        public int? AssetID { get; set; }
        public string AllocationID { get; set; }
        public string LocationCode { get; set; }
        public string Location { get; set; }
        public string MainAssetNumber { get; set; }
        public string AssetSubNumber { get; set; }
        public string AssetClass { get; set; }
        public string AssetDesc { get; set; }
        public string AdditionalDesc { get; set; }
        public string StatusDesc { get; set; }
        public string AssetRequestID { get; set; }
        public string CustodianDepartment { get; set; }
        public string AllocatedDate { get; set; }
        public string RequestedBy { get; set; }
        public string EmployeeID { get; set; }
        public string RequestedDate { get; set; }
        public string Status { get; set; }
        public string CustDesignation { get; set; }
        //public string SerialNumber { get; set; }
        //public string AssetClassID { get; set; }
        //public string AssetClassName { get; set; }
        //public string AssetClassCode { get; set; }
        //public string AssetTypeName { get; set; }
        //public string AssetTypeCode { get; set; }
        //public string AssetTypeID { get; set; }
        //public string QRImage { get; set; }
        //public string QRCode { get; set; }
        //public string StatusID { get; set; }
        //public string StatusName { get; set; }
        //public string StatusCode { get; set; }
    }
}