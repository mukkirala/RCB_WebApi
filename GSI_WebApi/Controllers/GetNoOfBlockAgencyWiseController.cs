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
    public class GetNoOfBlockAgencyWiseController : ApiController
    {
        public List<AgencyWiseBlock> Get(string AgencyID)
        {
            List<AgencyWiseBlock> myBlocks = new List<AgencyWiseBlock>();
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GSIConnectionString"].ConnectionString))
            {
                string sqlString = "Get_No_Of_Block_AgencyWise";
                using (SqlCommand cmd = new SqlCommand(sqlString, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AgencyID", AgencyID);
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    while (rdr.Read())
                    {
                        AgencyWiseBlock block = new AgencyWiseBlock();
                        block.Agency = rdr.GetValue(2).ToString();
                        block.UNFCStage = rdr.GetValue(1).ToString();
                        block.UNFCCount = Convert.ToInt32(rdr.GetValue(0).ToString());                     

                        myBlocks.Add(block);
                    }
                    conn.Close();
                    return myBlocks;
                }
            }
        }
    }
}
