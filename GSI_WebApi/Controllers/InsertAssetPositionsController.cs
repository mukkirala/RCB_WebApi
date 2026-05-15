using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GSI_WebApi.Models;
using System.Web.Configuration;
using System.Data.SqlClient;

namespace GSI_WebApi.Controllers
{
    public class InsertAssetPositionsController : ApiController
    {
        [HttpPost]
        public string InsertPositions(AssetPositions data)
        {
            try
            {
                SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["NRLSAPConnectionString"].ConnectionString);
                SqlCommand cmd = null;
                string statusval = "";
                string sqlState = "UPDATE AssetMaster SET Latitude=@Latitude, Longitude=@Longitude Where AssetID='" + data.AssetID + "'";
                cmd = new SqlCommand(sqlState, conn);
                cmd.Parameters.AddWithValue("@Latitude", data.lat);
                cmd.Parameters.AddWithValue("@Longitude", data.lng);               
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                statusval = "1";
                return statusval;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
