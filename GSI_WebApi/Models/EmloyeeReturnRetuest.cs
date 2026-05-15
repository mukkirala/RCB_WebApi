using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GSI_WebApi.Models
{
    public class EmloyeeReturnRetuest
    {
        public int AssetReturnID { get; set; }
        public string AssetID { get; set; }
        public string EmployeeID { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
        public string CustodianComments { get; set; }
        public string ApproverComments { get; set; }
        public string ToCustodian { get; set; }
        public string ToLocation { get; set; }
        public string AdminComments { get; set; }
    }
}