using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NRL_WebApi.Models
{
    public class HOD_DepartmentLogin
    {
        public int USR_ID { get; set; }
        public string USR_LOGIN { get; set; }
        public string USR_PASSWORD { get; set; }
        public string Department { get; set; }
        public string SecondDepartment { get; set; }
        public string USR_PHONE_NO { get; set; }
    }
}