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
    public class GetNoOfBlockAllAgencyController : ApiController
    {
        public List<GetNoOfBlockAllAgency> Get()
        {
            List<GetNoOfBlockAllAgency> myBlocks = new List<GetNoOfBlockAllAgency>();
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GSIConnectionString"].ConnectionString))
            {
                string sqlString = "Get_No_Of_Block_All_Agency";
                using (SqlCommand cmd = new SqlCommand(sqlString, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    while (rdr.Read())
                    {
                        GetNoOfBlockAllAgency block = new GetNoOfBlockAllAgency();
                        block.name = rdr.GetValue(0).ToString();
                        block.G2 = Convert.ToInt32(rdr.GetValue(1).ToString());
                        block.G3 = Convert.ToInt32(rdr.GetValue(2).ToString());
                        block.G4 = Convert.ToInt32(rdr.GetValue(3).ToString());
                        myBlocks.Add(block);
                    }
                    conn.Close();
                    return myBlocks;
                }
            }
        }
    }
}
