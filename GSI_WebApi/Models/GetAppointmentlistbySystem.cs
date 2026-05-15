using System;

namespace NRL_WebApi.Models
{
    public class GetAppointmentlistbySystem
    {
        public int VisitorHistoryID { get; set; }
        public int VisitorID { get; set; }
        public int OrganizationID { get; set; }
        public int SecondOrganizationID { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string PhoneNo { get; set; }
        public string PurposeOfVisit { get; set; }
        public DateTime DateTime { get; set; }
        public string Status { get; set; }
        public string OTP { get; set; }
    }
}