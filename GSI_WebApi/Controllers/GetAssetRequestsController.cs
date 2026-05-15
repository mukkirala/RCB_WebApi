using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;
using GSI_WebApi.Models;
using System.Data.SqlClient;
using System.Data;

namespace GSI_WebApi.Controllers
{
    public class GetAssetRequestsController : ApiController
    {
        [HttpPost]  
        public List<AssetRequest> Get(AssetRequest data)
        {
            List<AssetRequest> AssetReq = new List<AssetRequest>();
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["VisitorConnectionString"].ConnectionString))
            {

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "Select AssetRequestID,EmployeeAssetRequest.AssetTypeID, EmployeeAssetRequest.AssetTypeCode,AssetTypeName,RequestBy,EmployeeID,Quantity,EmployeeAssetRequest.Status,Date from RCBAMS..EmployeeAssetRequest inner join RCBSAP..AssetTypeMaster on AssetTypeMaster.AssetTypeID = EmployeeAssetRequest.AssetTypeID where EmployeeAssetRequest.Status !='Approved' and EmployeeAssetRequest.EmployeeID='" + data.EmployeeID + "' order by Date desc";
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    while (rdr.Read())
                    {
                        AssetRequest req = new AssetRequest();
                        req.AssetRequestID = Convert.ToInt32(rdr["AssetRequestID"]);
                        req.AssetTypeID = rdr["AssetTypeID"].ToString();
                        req.AssetTypeCode = rdr["AssetTypeCode"].ToString();
                        req.AssetTypeName = rdr["AssetTypeName"].ToString();
                        req.RequestBy = rdr["RequestBy"].ToString();
                        req.EmployeeID = rdr["EmployeeID"].ToString();
                        req.Quantity = rdr["Quantity"].ToString();
                        req.Status = rdr["Status"].ToString();
                       
                        string date1 = rdr["Date"].ToString();
                        req.Date = Convert.ToDateTime(date1).ToString("dd-MMM-yyyy");
                        AssetReq.Add(req);
                    }
                    conn.Close();
                    return AssetReq;
                }
            }
        }
    }
}
