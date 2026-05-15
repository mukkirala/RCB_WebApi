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
    public class GetAgencyWiseBlockAreaDMController : ApiController
    {
        public List<GetAgencyWiseBlockAreaDM> Get(string AgencyID)
        {
            List<GetAgencyWiseBlockAreaDM> myBlocks = new List<GetAgencyWiseBlockAreaDM>();
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GSIConnectionString"].ConnectionString))
            {
                string sqlString = "Get_Agency_Wise_BlockArea_DM";
                using (SqlCommand cmd = new SqlCommand(sqlString, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AgencyID", AgencyID);
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    while (rdr.Read())
                    {
                        GetAgencyWiseBlockAreaDM block = new GetAgencyWiseBlockAreaDM();

                        block.UNFCCount = Convert.ToInt32(rdr.GetValue(1).ToString());                        
                        block.DM_Area_SQ_KM = rdr.GetValue(0).ToString();

                        myBlocks.Add(block);
                    }
                    conn.Close();
                    return myBlocks;
                }
            }
        }
    }
}
