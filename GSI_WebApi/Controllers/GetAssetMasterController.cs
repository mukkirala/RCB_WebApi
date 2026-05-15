using NRLWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;
using GSI_WebApi.Models;

namespace GSI_WebApi.Controllers
{
    public class GetAssetMasterController : ApiController
    {
        [HttpGet]
        public List<GetAssetMaster> Get()
        {
            List<GetAssetMaster> Appointment = new List<GetAssetMaster>();
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["NRLSAPConnectionString"].ConnectionString))
            {

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select SLNO,AssetID,MainAssetNumber,AssetSubNumber,AssetDesc from AssetMaster ";
                  
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
                       // Appointid.AdditionalDesc = rdr["AdditionalDesc"].ToString();
                        //Appointid.Unit = rdr["Unit"].ToString();
                        //Appointid.Location = rdr["Location"].ToString();
                        //Appointid.Quantity = rdr["Quantity"].ToString();
                        //Appointid.AssetCapitalizationDate = rdr["AssetCapitalizationDate"].ToString();
                        //Appointid.FirstAcquisitionDate = rdr["FirstAcquisitionDate"].ToString();
                        //Appointid.AssetCreatedDate = Convert.ToDateTime(rdr["AssetCreatedDate"].ToString());
                        //Appointid.CustodianID = rdr["CustodianID"].ToString();
                        //Appointid.AssetCapitalizationDate = Convert.ToDateTime(rdr["AssetCapitalizationDate"].ToString());
                        //Appointid.Location = rdr["Location"].ToString();
                        //Appointid.LocationDesc = rdr["LocationDesc"].ToString();
                        //Appointid.Status = rdr["Status"].ToString();
                        //Appointid.StatusDesc = rdr["StatusDesc"].ToString();
                        //Appointid.AssetClass = rdr["AssetClass"].ToString();
                        //Appointid.Component = rdr["Component"].ToString();
                        //Appointid.ComponentDesc = rdr["ComponentDesc"].ToString();
                        //Appointid.Deacdate = rdr["Deacdate"].ToString();
                        //Appointid.Invdate = rdr["Invdate"].ToString();
                        //Appointid.InventoryNote = rdr["InventoryNote"].ToString();
                        //Appointid.QRCode = rdr["QRCode"].ToString();
                        //Appointid.QRImage = rdr["QRImage"].ToString();
                        //Appointid.Latitude = rdr["Latitude"].ToString();
                        //Appointid.Longitude = rdr["Longitude"].ToString();
                        Appointment.Add(Appointid);
                    }
                    conn.Close();
                    return Appointment;
                }
            }
        }
    }
}
