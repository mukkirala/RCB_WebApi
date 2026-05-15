using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NRL_WebApi.Models
{
    public class ViewGSIData
    {
        public string ProjectID { get; set; }
        public string Agency { get; set; }
        public string StateName { get; set; }
        public string DistrictName { get; set; }
        public string Mineral_Tectonic_Belt { get; set; }
        public string Block_Name { get; set; }
        public string Toposheet_No { get; set; }
        public string Commodity { get; set; }
        public string UNFCStage { get; set; }
        public string CommodityType { get; set; }
        public string Grade { get; set; }
        public string ProjectNo { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string EndYear { get; set; }
        public string NoOfBoreholes { get; set; }
        public string DrillingInMeter { get; set; }
        public string DM_Area_SQ_KM { get; set; }
        public string LSM_Area_SQ_KM { get; set; }
        public string Resources { get; set; }
        public string GeologyOfTheArea { get; set; }
        public string Status { get; set; }
        public string StartYear { get; set; }
        public string Rownumber { get; set; }
    }
}