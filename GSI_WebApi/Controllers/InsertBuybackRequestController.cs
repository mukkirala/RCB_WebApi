using GSI_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Data;

namespace GSI_WebApi.Controllers
{
    public class InsertBuybackRequestController : ApiController
    {
        [HttpPost]
        public string checkuserRegistration(BuybackRequest data)
        {
            try
            {
                SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["VisitorConnectionString"].ConnectionString);
                SqlCommand cmd = null;
                string statusval = "";
                string authid = GetReportingAuth(data.EmployeeID);
                string sqlState = "insert into EmployeeAssetBuyback(AssetID,EmployeeID,Comments,Status,Date,RequestBy,CustodianDepartment,CustDepartmentCode,CustDesignation,ApproverID,RequestType)values(@AssetID,@EmployeeID,@Comments,@Status,@Date,@RequestBy,@CustodianDepartment,@CustDepartmentCode,@CustDesignation,@ApproverID,@RequestType)";
                cmd = new SqlCommand(sqlState, conn);
                cmd.Parameters.AddWithValue("@AssetID", data.AssetID);
                cmd.Parameters.AddWithValue("@RequestBy", data.RequestBy);
                cmd.Parameters.AddWithValue("@EmployeeID", data.EmployeeID);
                cmd.Parameters.AddWithValue("@Comments", data.Comments);
                cmd.Parameters.AddWithValue("@Status", data.Status);
                cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                cmd.Parameters.AddWithValue("@CustodianDepartment", data.CustodianDepartment);
                cmd.Parameters.AddWithValue("@CustDepartmentCode", data.CustDepartmentCode);
                cmd.Parameters.AddWithValue("@CustDesignation", data.CustDesignation);
                cmd.Parameters.AddWithValue("@ApproverID", authid);             
                cmd.Parameters.AddWithValue("@RequestType", "Buyback");
                conn.Open();
                cmd.ExecuteNonQuery();
                string admin = GetAdminCustodianID();
                string asset = GetAssetType(data.AssetID.ToString());
                string main = GetAssetMain(data.AssetID.ToString());
                string subject = "Request For Asset BuyBack";
                string msg2 = "New Request of Asset Buyback from Employee ID: <b>" + data.EmployeeID + ", " + data.RequestBy + "</b> for Asset with MainAssetNumber: <b>" + main + "</b> and Asset Description: <b>" + asset + "</b>.<br/><br/>" + "<b>Please verify to complete the approval process.</b>";

                bool response = MailSending.sendMailAttach(admin, subject, msg2);
                if (response == true)
                {
                  
                }
                conn.Close();
                statusval = "1";
                return statusval;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string GetAssetMain(string astid)
        {
            SqlConnection myConnectionString = new SqlConnection(WebConfigurationManager.ConnectionStrings["NRLSAPConnectionString"].ConnectionString);
            string rmail = "";
            string query = "SELECT MainAssetNumber FROM AssetMaster where AssetID = '" + astid + "'";
            SqlDataAdapter da = new SqlDataAdapter(query, myConnectionString);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                rmail = dt.Rows[0]["MainAssetNumber"].ToString();
            }
            return rmail;
        }

        public string GetAssetType(string astid)
        {
            SqlConnection myConnectionString = new SqlConnection(WebConfigurationManager.ConnectionStrings["NRLSAPConnectionString"].ConnectionString);
            string rmail = "";
            string query = "SELECT AssetDesc FROM AssetMaster where AssetID = '" + astid + "'";
            SqlDataAdapter da = new SqlDataAdapter(query, myConnectionString);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                rmail = dt.Rows[0]["AssetDesc"].ToString();
            }
            return rmail;
        }

        public string GetAdminCustodianID()
        {
            SqlConnection conAMS = new SqlConnection(WebConfigurationManager.ConnectionStrings["VisitorConnectionString"].ConnectionString);
            string id = "";
            string query = "SELECT CustodianID FROM [RoleMaster] where ROLE_NAME ='Admin' and ROLE_STATUS='Active'";
            SqlDataAdapter da = new SqlDataAdapter(query, conAMS);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                id = dt.Rows[0]["CustodianID"].ToString();
            }
            return id;
        }

        public string GetReportingAuth(string custid)
        {
            SqlConnection myConnectionString = new SqlConnection(WebConfigurationManager.ConnectionStrings["ASSETManagementConnectionString"].ConnectionString);
            string rmail = "";
            string query = "SELECT reporting_staff_no FROM [vEmpDtlsAssetApp] where CustodianID = '" + custid + "'";
            SqlDataAdapter da = new SqlDataAdapter(query, myConnectionString);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                rmail = dt.Rows[0]["reporting_staff_no"].ToString();
            }
            return rmail;
        }


        //public bool InsertRequest(IEnumerable<BuybackRequest> data)
        //{
        //    using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["VisitorConnectionString"].ConnectionString))
        //    {
        //        SqlCommand cmd = null;
        //        string sqlState1 = "insert into EmployeeAssetBuyback(AssetID,EmployeeID,Comments,Status,Date)values(@AssetID,@EmployeeID,@Comments,@Status,@Date)";
        //        cmd = new SqlCommand(sqlState1, conn);
        //        foreach (var req in data)
        //        {
        //            cmd.Parameters.Clear();

        //            cmd.Parameters.AddWithValue("@AssetID", req.AssetID);
        //            //cmd.Parameters.AddWithValue("@RequestBy", req.RequestBy);
        //            cmd.Parameters.AddWithValue("@EmployeeID", req.EmployeeID);
        //            cmd.Parameters.AddWithValue("@Comments", req.Comments);
        //            cmd.Parameters.AddWithValue("@Status", req.Status);
        //            cmd.Parameters.AddWithValue("@Date", DateTime.Now);
        //            conn.Open();
        //            int x = cmd.ExecuteNonQuery();
        //            conn.Close();

        //        }
        //    }
        //    return true;
        //}
    }
}