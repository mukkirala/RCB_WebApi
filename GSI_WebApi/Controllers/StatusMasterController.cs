using NRL_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;

namespace GSI_WebApi.Controllers
{
    public class StatusMasterController: ApiController
    {
        public List<StatusMaster> Get()
        {
            List<StatusMaster> status = new List<StatusMaster>();
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["VisitorConnectionString"].ConnectionString))
            {

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT StatusID,[StatusCode],[StatusName],Status FROM [RCBAMS]..[StatusMaster]";
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    while (rdr.Read())
                    {
                        StatusMaster statusid = new StatusMaster();
                        statusid.StatusID = Convert.ToInt32(rdr["StatusID"]);
                        statusid.StatusCode = rdr["StatusCode"].ToString();
                        statusid.StatusName = rdr["StatusName"].ToString();
                        statusid.Status = rdr["Status"].ToString();
                        status.Add(statusid);
                    }
                    conn.Close();
                    return status;
                }
            }
        }
    }
}