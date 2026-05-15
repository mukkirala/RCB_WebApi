using NRL_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;

namespace NRL_WebApi.Controllers
{
    public class GetReferanceOfEmployeeController : ApiController
    {
        [HttpPost]
        public List<GetReferanceOfEmployee> Get(GetReferanceOfEmployee data)
        {
            List<GetReferanceOfEmployee> Employees = new List<GetReferanceOfEmployee>();
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["VisitorConnectionString"].ConnectionString))
            {

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT  EmployeeID, EmployeeName FROM EmployeeMaster WHERE OrganizationID='" + data.OrganizationID + "'";
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    while (rdr.Read())
                    {
                        GetReferanceOfEmployee emp = new GetReferanceOfEmployee();
                        emp.EmployeeID = Convert.ToInt32(rdr["EmployeeID"]);
                        emp.EmployeeName = rdr["EmployeeName"].ToString();
                        // dept.OrganizationID = rdr["OrganizationID"].ToString();
                        Employees.Add(emp);
                    }
                    conn.Close();
                    return Employees;
                }
            }
        }
    }
}
