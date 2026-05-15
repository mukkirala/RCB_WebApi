using GSI_WebApi.Models;
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
    public class GetAllocatedAssetsController : ApiController
    {
        [HttpPost]
        //public List<AuditMaster> Get(AuditMaster data)
        //{
        //    List<AuditMaster> audit = new List<AuditMaster>();  AllocatedAssets
        public List<AllocatedAssets> Get(AllocatedAssets data)
        {
            List<AllocatedAssets> Appointment = new List<AllocatedAssets>();
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["NRLSAPConnectionString"].ConnectionString))
            {

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "Select AssetID,MainAssetNumber,AssetSubNumber,CustodianDepartment,AssetDesc,AdditionalDesc,CustodianID,Location,LocationDesc,Status,StatusDesc,AssetClass From RCBSAP..vAssetMaster where CustodianID='" + data.EmployeeID + "' and Status !='NVAL'";
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    while (rdr.Read())
                    {
                        AllocatedAssets Appointid = new AllocatedAssets();
                        Appointid.AssetID = Convert.ToInt32(rdr["AssetID"]);
                        Appointid.LocationCode = rdr["Location"].ToString();
                        Appointid.Location = rdr["LocationDesc"].ToString();
                        Appointid.MainAssetNumber = rdr["MainAssetNumber"].ToString();
                        Appointid.AssetSubNumber = rdr["AssetSubNumber"].ToString();
                        Appointid.AssetClass = rdr["AssetClass"].ToString();
                        Appointid.AssetDesc = rdr["AssetDesc"].ToString();
                        Appointid.AdditionalDesc = rdr["AdditionalDesc"].ToString();
                        Appointid.Status = rdr["Status"].ToString();
                        Appointid.StatusDesc = rdr["StatusDesc"].ToString();
                        Appointid.CustodianDepartment = rdr["CustodianDepartment"].ToString();
                        Appointid.EmployeeID = rdr["CustodianID"].ToString();
                        Appointment.Add(Appointid);
                    }
                    conn.Close();
                    return Appointment;
                }
            }
        }
    }
}