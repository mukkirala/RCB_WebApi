using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NRL_WebApi.Models
{
    public class GetReferanceOfEmployee
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string OrganizationID { get; set; }
    }
}