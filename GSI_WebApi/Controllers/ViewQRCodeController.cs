using NRLWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;

namespace NRLWebApi.Controllers
{
    public class ViewQRCodeController : ApiController
    {
        [HttpGet]
        public List<ViewQRCodeModel> Get()
        {
            List<ViewQRCodeModel> Appointment = new List<ViewQRCodeModel>();
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["VisitorConnectionString"].ConnectionString))
            {

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "Select RCBSAP..AssetMaster.AssetID,AssetMaster.MainAssetNumber,AssetMaster.AssetSubNumber,CustodianDepartment,AssetDesc,AdditionalDesc,Unit,Quantity,AssetCapitalizationDate,FirstAcquisitionDate,AssetMaster.CustodianID,Location,LocationDesc,Status,StatusDesc,AssetClass,Component,ComponentDesc,Deacdate,Invdate,InventoryNote,QRImage,QRCode,Latitude,Longitude from RCBSAP..AssetMaster left JOIN RCBAMS..QRCodeMaster ON AssetMaster.AssetID=QRCodeMaster.AssetID";
                   // cmd.CommandText = "Select RCBSAP..AssetMaster.AssetID,Client,CompanyCode,RCBSAP..AssetMaster.MainAssetNumber,RCBSAP..AssetMaster.AssetSubNumber,AssetClass,TechnicalAssetNumber,AssetType ,AssetCreatedBy,AssetCatagory ,AssetOrigin,AssetSupplier,AssetManufacturer,RCBSAP..AssetMaster.Status,FiscalYear,SerialNumber,AssetClassID,AssetClassName,AssetClassCode,AssetTypeID,AssetTypeName,AssetTypeCode,QRImage,QRCode,StatusID,StatusCode,StatusName from RCBSAP..AssetMaster left JOIN RCBAMS..QRCodeMaster ON AssetMaster.AssetID=QRCodeMaster.AssetID left JOIN RCBSAP..AssetClassMaster ON AssetMaster.AssetClass=AssetClassMaster.AssetClassCode left JOIN RCBSAP..AssetTypeMaster ON AssetMaster.AssetType=AssetTypeMaster.AssetTypeCode left JOIN RCBAMS..StatusMaster ON StatusMaster.StatusCode=AssetMaster.Status";
                    //Select * from RCBSAP..AssetMaster left JOIN RCBAMS..QRCodeMaster ON AssetMaster.MainAssetNumber=QRCodeMaster.MainAssetNumber left JOIN RCBSAP..AssetClassMaster ON AssetMaster.AssetClass=AssetClassMaster.AssetClassCode left JOIN RCBSAP..AssetTypeMaster ON AssetMaster.AssetType=AssetTypeMaster.AssetTypeCode left JOIN RCBAMS..StatusMaster ON StatusMaster.StatusCode=AssetMaster.Status
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    while (rdr.Read())
                    {
                        ViewQRCodeModel Appointid = new ViewQRCodeModel();
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
                        Appointid.QRCode= rdr["QRCode"].ToString();
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