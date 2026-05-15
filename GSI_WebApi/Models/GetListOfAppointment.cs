using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NRL_WebApi.Models
{
    public class GetListOfAppointment
    {
        public int AppointmentID { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string PurposeofVisit { get; set; }
        public string HostName { get; set; }
        public int DepartmentID { get; set; }
        public int SecondDepartment { get; set; }
        public string Status { get; set; }
        public DateTime? Date { get; set; }
    }
}