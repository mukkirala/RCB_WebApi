using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NRLWebApi.Models
{
    public class ViewQRCodeModel
    {
        public int? AssetID { get; set; }
        public string MainAssetNumber { get; set; }
        public string AssetSubNumber { get; set; }
        public string CustodianDepartment { get; set; }
        public string AssetDesc { get; set; }
        public string AdditionalDesc { get; set; }
        public string Unit { get; set; }
        public string Quantity { get; set; }
        public string AssetCapitalizationDate { get; set; }
      
        public string FirstAcquisitionDate { get; set; }
      
        public string CustodianID { get; set; }
        public string Location { get; set; }
        public string LocationDesc { get; set; }
        public string Status { get; set; }
        public string StatusDesc { get; set; }
        public string AssetClass { get; set; }
        public string Component { get; set; }
        public string ComponentDesc { get; set; }
        public string Deacdate { get; set; }
        public string Invdate { get; set; }
        public string InventoryNote { get; set; }
      
        public string QRImage { get; set; }
        public string QRCode { get; set; }

        public string Latitude { get; set; }
        public string Longitude { get; set; }

    }
}

