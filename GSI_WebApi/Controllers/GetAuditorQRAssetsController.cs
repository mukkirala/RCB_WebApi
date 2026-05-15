using GSI_WebApi.Models;
using NRLWebApi.Models;
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
    public class GetAuditorQRAssetsController : ApiController
    {
        [HttpPost]
        //public List<AuditMaster> Get(AuditMaster data)
        //{
        //    List<AuditMaster> audit = new List<AuditMaster>();
        public List<ViewRequesterQRCodeModel> Get(ViewRequesterQRCodeModel data)
        {
            List<ViewRequesterQRCodeModel> Appointment = new List<ViewRequesterQRCodeModel>();
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["NRLSAPConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    // cmd.CommandText = "Select RCBSAP..AssetMaster.AssetID,AssetMaster.MainAssetNumber,AssetMaster.AssetSubNumber,AssetMaster.CustodianDepartment,AssetDesc,AdditionalDesc,Unit,AssetMaster.Quantity,AssetCapitalizationDate,FirstAcquisitionDate,AssetMaster.CustodianID,Location,LocationDesc,AssetMaster.Status,StatusDesc,AssetClass,Component,ComponentDesc,Deacdate,Invdate,InventoryNote,QRImage,QRCode,Latitude,Longitude From RCBAMS..AssetAllocation inner join RCBAMS..EmployeeAssetRequest on EmployeeAssetRequest.AssetRequestID=AssetAllocation.AssetRequestID inner join RCBSAP..AssetMaster on AssetMaster.AssetID=AssetAllocation.AssetID left JOIN RCBAMS..QRCodeMaster ON AssetMaster.AssetID=QRCodeMaster.AssetID where AssetAllocation.Status='Approved' and AssetMaster.CustodianID='" + data.EmployeeID + "'";
                    cmd.CommandText = "Select RCBSAP..AssetMaster.AssetID,AssetMaster.MainAssetNumber,AssetMaster.AssetSubNumber,CustodianDepartment,AssetDesc,AdditionalDesc,Unit,Quantity,AssetCapitalizationDate,FirstAcquisitionDate,AssetMaster.CustodianID,Location,LocationDesc,AssetMaster.Status,StatusDesc,AssetClass,Component,ComponentDesc,Deacdate,Invdate,InventoryNote,QRImage,QRCode,Latitude,Longitude from RCBSAP..AssetMaster left JOIN RCBAMS..QRCodeMaster ON AssetMaster.AssetID = QRCodeMaster.AssetID inner join RCBSAP..AssetClassMaster on AssetClassMaster.AssetClassName = AssetMaster.AssetClass inner join RCBAMS..RoleMaster on RoleMaster.AssetClassID = AssetClassMaster.AssetClassID where RoleMaster.ROLE_NAME = 'Auditor'and RoleMaster.CustodianID = '" + data.EmployeeID + "' and AssetClassMaster.Status = 'Active' and RoleMaster.ROLE_STATUS = 'Active'";

                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    while (rdr.Read())
                    {
                        ViewRequesterQRCodeModel Appointid = new ViewRequesterQRCodeModel();
                        Appointid.AssetID = Convert.ToInt32(rdr["AssetID"]);
                        Appointid.MainAssetNumber = rdr["MainAssetNumber"].ToString();
                        Appointid.AssetSubNumber = rdr["AssetSubNumber"].ToString();
                        Appointid.CustodianDepartment = rdr["CustodianDepartment"].ToString();
                        Appointid.AssetDesc = rdr["AssetDesc"].ToString();
                        Appointid.AdditionalDesc = rdr["AdditionalDesc"].ToString();
                        Appointid.Unit = rdr["Unit"].ToString();
                        Appointid.Location = rdr["Location"].ToString();
                        Appointid.Quantity = rdr["Quantity"].ToString();
                        Appointid.AssetCapitalizationDate = rdr["AssetCapitalizationDate"].ToString();
                        Appointid.FirstAcquisitionDate = rdr["FirstAcquisitionDate"].ToString();
                        //Appointid.AssetCreatedDate = Convert.ToDateTime(rdr["AssetCreatedDate"].ToString());
                        Appointid.CustodianID = rdr["CustodianID"].ToString();
                        //Appointid.AssetCapitalizationDate = Convert.ToDateTime(rdr["AssetCapitalizationDate"].ToString());
                        Appointid.Location = rdr["Location"].ToString();
                        Appointid.LocationDesc = rdr["LocationDesc"].ToString();
                        Appointid.Status = rdr["Status"].ToString();
                        Appointid.StatusDesc = rdr["StatusDesc"].ToString();
                        Appointid.AssetClass = rdr["AssetClass"].ToString();
                        Appointid.Component = rdr["Component"].ToString();
                        Appointid.ComponentDesc = rdr["ComponentDesc"].ToString();
                        Appointid.Deacdate = rdr["Deacdate"].ToString();
                        Appointid.Invdate = rdr["Invdate"].ToString();
                        Appointid.InventoryNote = rdr["InventoryNote"].ToString();
                        Appointid.QRCode = rdr["QRCode"].ToString();
                        Appointid.QRImage = rdr["QRImage"].ToString();
                        Appointid.Latitude = rdr["Latitude"].ToString();
                        Appointid.Longitude = rdr["Longitude"].ToString();
                        Appointment.Add(Appointid);

                    }
                    conn.Close();
                    return Appointment;
                }
            }
        }
    }
}
