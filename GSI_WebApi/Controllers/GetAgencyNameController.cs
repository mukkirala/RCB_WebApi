using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NRL_WebApi.Models;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;

namespace NRL_WebApi.Controllers
{
    public class GetAgencyNameController : ApiController
    {
        public List<GetAgencyName> Get()
        {
            List<GetAgencyName> Agency = new List<GetAgencyName>();
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GSIConnectionString"].ConnectionString))
            {
               
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT  distinct AgencyID, Agency FROM AgencyMaster";
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    while (rdr.Read())
                    {
                        GetAgencyName agencyname = new GetAgencyName();
                        agencyname.AgencyID = Convert.ToInt32(rdr["AgencyID"]);
                        agencyname.Agency = rdr["Agency"].ToString();
                        Agency.Add(agencyname);
                    }
                    conn.Close();
                    return Agency;
                }
            }
        }
    }
}
