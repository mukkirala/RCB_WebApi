using NRL_WebApi.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.Http;

namespace NRL_WebApi.Controllers
{
    public class UserRegistrationController : ApiController
    {
        [HttpPost]
        public string checkuserRegistration(UserRegistration data)
        {
            try
            {
                SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["VisitorConnectionString"].ConnectionString);
                SqlCommand cmd = null;
                string statusval = "";

                string query = "select * from AuditDetails where AssetID='" + data.AssetID + "' and AuditID='" + data.AuditID + "'";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    string query1 = "update AuditDetails set AuditStatus=@AuditStatus,Comments=@Comments,AuditBy=@AuditBy,Location=@Location,AuditID=@AuditID,Status=@Status,CustodianID=@CustodianID,MainAssetNumber=@MainAssetNumber,AssetClassID=@AssetClassID where AssetID=@AssetID";
                    cmd = new SqlCommand(query1, conn);
                    cmd.Parameters.AddWithValue("@AuditStatus", data.Status);
                    cmd.Parameters.AddWithValue("@Comments", data.Comments);
                    cmd.Parameters.AddWithValue("@AssetID", data.AssetID);
                    cmd.Parameters.AddWithValue("@AuditID", data.AuditID);
                    cmd.Parameters.AddWithValue("@Status", "Audited");
                    cmd.Parameters.AddWithValue("@CustodianID", data.CustodianID);
                    cmd.Parameters.AddWithValue("@AuditBy", data.AuditBy);
                    cmd.Parameters.AddWithValue("@Location", data.Location);
                    cmd.Parameters.AddWithValue("@MainAssetNumber", data.MainAssetNumber);
                    cmd.Parameters.AddWithValue("@AssetClassID", data.AssetClassID);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();                                   
                }
                else
                {
                    string sqlState = "insert into [AuditDetails](MainAssetNumber,Status,Comments,AuditID,Date,AssetID,CustodianID,AuditStatus,Location,AuditBy,AssetClassID)values(@MainAssetNumber,@Status,@Comments,@AuditID,@Date,@AssetID,@CustodianID,@AuditStatus,@Location,@AuditBy,@AssetClassID)";
                    cmd = new SqlCommand(sqlState, conn);
                    cmd.Parameters.AddWithValue("@MainAssetNumber", data.MainAssetNumber);
                    cmd.Parameters.AddWithValue("@Status", "Audited");
                    cmd.Parameters.AddWithValue("@Comments", data.Comments);
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                    cmd.Parameters.AddWithValue("@AuditID", data.AuditID);
                    cmd.Parameters.AddWithValue("@AssetID", data.AssetID);
                    cmd.Parameters.AddWithValue("@CustodianID", data.CustodianID);
                    cmd.Parameters.AddWithValue("@AuditStatus", data.Status);
                    cmd.Parameters.AddWithValue("@Location", data.Location);
                    cmd.Parameters.AddWithValue("@AuditBy", data.AuditBy);
                    cmd.Parameters.AddWithValue("@AssetClassID", data.AssetClassID);
                    //cmd.Parameters.AddWithValue("@Company", data.Company);
                    //cmd.Parameters.AddWithValue("@Designation", data.Designation);
                    //cmd.Parameters.AddWithValue("@Profession", data.Profession);
                    //cmd.Parameters.AddWithValue("@PurposeofVisit", data.PurposeofVisit);
                    //cmd.Parameters.AddWithValue("@RegDate", DateTime.Now);
                    //cmd.Parameters.AddWithValue("@Status", "Pending");
                    //cmd.Parameters.AddWithValue("@UserID", data.UserID);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                statusval = "1";
                return statusval;

                //SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["VisitorConnectionString"].ConnectionString);
                //DataTable dt = new DataTable();
                //string sqlcheckuser = "SELECT  UserID FROM UserRegistration WHERE MobileNo='" + data.MobileNo + "'";
                //SqlCommand cmd = new SqlCommand(sqlcheckuser, conn);
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //conn.Open();
                //da.Fill(dt);
                //conn.Close();
                //string statusval = "";
                //if (dt.Rows.Count == 0)
                //{
                //    string sqlState = "Insert into UserRegistration (Username,MobileNo, Password) VALUES(@Username,@MobileNo,@Password)";
                //    cmd = new SqlCommand(sqlState, conn);
                //    cmd.Parameters.AddWithValue("@Username", data.Username);
                //    cmd.Parameters.AddWithValue("@MobileNo", data.MobileNo);
                //    cmd.Parameters.AddWithValue("@Password", data.Password);
                //    conn.Open();
                //    cmd.ExecuteNonQuery();
                //    conn.Close();
                //    statusval = "1";
                //}
                //else
                //{
                //    statusval = "0";
                //}
                //return statusval;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
