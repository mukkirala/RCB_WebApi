using System;

namespace NRL_WebApi.Models
{
    public class ViewInQRCode
    {
        public int VisitorHistoryID { get; set; }
        public int VisitorID { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string CompanyName { get; set; }
        public string PhoneNo { get; set; }
        public string PurposeOfVisit { get; set; }       
        public string Duration { get; set; }
        public string NoOfVisitors { get; set; }
        public DateTime DateTime { get; set; }
        public string OrganizationName { get; set; }

    }
}