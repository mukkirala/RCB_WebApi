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
    public class StateWiseInProgressController : ApiController
    {
        public List<StateWiseInProgress> Get()
        {
            List<StateWiseInProgress> myBlocks = new List<StateWiseInProgress>();
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GSIConnectionString"].ConnectionString))
            {
                string sqlString = "Get_UNFCCount_StateWise_InProgress";
                using (SqlCommand cmd = new SqlCommand(sqlString, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    while (rdr.Read())
                    {
                        StateWiseInProgress block = new StateWiseInProgress();
                        block.UNFCStage = rdr.GetValue(2).ToString();
                        block.UNFCCount = Convert.ToInt32(rdr.GetValue(1).ToString());
                        block.StateName = rdr.GetValue(0).ToString();
                        myBlocks.Add(block);
                    }
                    conn.Close();
                    return myBlocks;
                }
            }
        }
    }
}
