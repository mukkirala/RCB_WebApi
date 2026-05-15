using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GSI_WebApi.Models;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace GSI_WebApi.Controllers
{
    public class CustodianController : ApiController
    {
        [HttpGet]
        public List<CustodianModel> Get()
        {
            List<CustodianModel> Appointment = new List<CustodianModel>();
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["ASSETManagementConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "Select * from vEmpDtlsAssetApp inner join vDepartmentAssetApp on vDepartmentAssetApp.DepartmentCode = vEmpDtlsAssetApp.CustodianDepartmentCode order by CustodianName ASC";
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    while (rdr.Read())
                    {
                        CustodianModel Appointid = new CustodianModel();
                        Appointid.CustodianID = rdr["CustodianID"].ToString();
                        Appointid.CustodianName = rdr["CustodianName"].ToString();
                        Appointid.CustodianDepartmentCode = rdr["CustodianDepartmentCode"].ToString();
                        Appointid.Designation = rdr["Designation"].ToString();
                        Appointid.DepartmentName = rdr["DepartmentName"].ToString();
                        Appointment.Add(Appointid);
                    }
                    conn.Close();
                    return Appointment;
                }
            }
        }
    }
}

