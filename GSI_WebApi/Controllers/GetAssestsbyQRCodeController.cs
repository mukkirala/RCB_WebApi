using GSI_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;

namespace GSI_WebApi.Controllers
{
    public class GetAssestsbyQRCodeController : ApiController
    {
        [HttpPost]
        public List<GetAssetMaster> Get(GetAssetMaster data)
        {
            List<GetAssetMaster> Appointment = new List<GetAssetMaster>();
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["NRLSAPConnectionString"].ConnectionString))
            {

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"select AssetMaster.LocationID,SLNO,AssetID,MainAssetNumber,AssetSubNumber,AssetDesc,LocationMaster.Location,LocationMaster.Block,LocationCode,RFIDCardNumber,AssetMaster.PONoforReference 
                                    from AssetMaster left join RCBAMS..LocationMaster on LocationMaster.LocationID=AssetMaster.LocationID

                                    where MainAssetNumber ='" + data.MainAssetNumber + "'";

                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    while (rdr.Read())
                    {
                        GetAssetMaster Appointid = new GetAssetMaster();
                        Appointid.SLNO = Convert.ToInt32(rdr["SLNO"]);
                        Appointid.AssetID = Convert.ToInt32(rdr["AssetID"]);
                        Appointid.MainAssetNumber = rdr["MainAssetNumber"].ToString();
                        Appointid.AssetSubNumber = rdr["AssetSubNumber"].ToString();
                        // Appointid.CustodianDepartment = rdr["CustodianDepartment"].ToString();
                        Appointid.AssetDesc = rdr["AssetDesc"].ToString();
                        Appointid.Location = rdr["Location"].ToString() + "-" + rdr["Block"].ToString() + "-" + rdr["LocationCode"].ToString();
                        Appointid.rfidTag = rdr["RFIDCardNumber"].ToString();
                        Appointid.PONoforReference = rdr["PONoforReference"].ToString();
                        Appointment.Add(Appointid);
                    }
                    conn.Close();
                    return Appointment;
                }
            }
        }
    }
}
