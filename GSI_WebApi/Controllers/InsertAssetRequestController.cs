using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;
using System.Data;
using GSI_WebApi.Models;

namespace GSI_WebApi.Controllers
{
    public class InsertAssetRequestController : ApiController
    {
        [HttpPost]
        public string checkuserRegistration(AssetRequest data)
        {
            try
            {
                SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["VisitorConnectionString"].ConnectionString);
                SqlConnection myConnectionString = new SqlConnection(WebConfigurationManager.ConnectionStrings["ASSETManagementConnectionString"].ConnectionString);
                SqlCommand cmd = null;
                string statusval = "";
                string authid = GetReportingAuth(data.EmployeeID);
                string sqlState = "Insert into EmployeeAssetRequest (AssetTypeID,AssetClassID,RequestBy,EmployeeID,Quantity,Status,Date,CustodianDepartment,CustDepartmentCode,CustDesignation,ApproverID,Location,RequestType) VALUES (@AssetTypeID,@AssetClassID,@RequestBy,@EmployeeID,@Quantity,@Status,@Date,@CustodianDepartment,@CustDepartmentCode,@CustDesignation,@ApproverID,@Location,@RequestType)";
                cmd = new SqlCommand(sqlState, conn);
                cmd.Parameters.AddWithValue("@AssetTypeID", data.AssetTypeID);
                cmd.Parameters.AddWithValue("@AssetClassID", data.AssetClassID);
                // cmd.Parameters.AddWithValue("@AssetTypeCode", data.AssetTypeCode);
                cmd.Parameters.AddWithValue("@RequestBy", data.RequestBy);
                cmd.Parameters.AddWithValue("@EmployeeID", data.EmployeeID);
                cmd.Parameters.AddWithValue("@Quantity", data.Quantity);
                if (authid == "")
                {
                    authid = GetAdminCustodianID();
                    cmd.Parameters.AddWithValue("@Status", "Request Sent To Admin");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Status", data.Status);
                }
                cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                cmd.Parameters.AddWithValue("@CustodianDepartment", data.CustodianDepartment);
                cmd.Parameters.AddWithValue("@CustDepartmentCode", data.CustDepartmentCode);
                cmd.Parameters.AddWithValue("@CustDesignation", data.CustDesignation);
                cmd.Parameters.AddWithValue("@ApproverID", authid);
                cmd.Parameters.AddWithValue("@Location", data.Location);
                cmd.Parameters.AddWithValue("@RequestType", "Asset Request");
                conn.Open();
                cmd.ExecuteNonQuery();

                string asset = GetAssetType(data.AssetTypeID);
                string location = GetLocation(data.Location);
                string subject = "Request For Asset Allocation";
                string msg2 = "New Request of Asset Allocation from Employee ID <b>" + data.EmployeeID + ", " + data.RequestBy + "</b> for Asset Type: <b>" + asset + "</b> to the location: <b>" + location + "</b>.<br/>" + "<b>Please verify to complete the approval process.</b>";
                bool response = MailSending.sendMailAttach(authid, subject, msg2);
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
        public string GetAssetType(string astid)
        {
            SqlConnection myConnectionString = new SqlConnection(WebConfigurationManager.ConnectionStrings["NRLSAPConnectionString"].ConnectionString);
            string rmail = "";
            string query = "SELECT AssetTypeName FROM AssetTypeMaster where AssetTypeID = '" + astid + "'";
            SqlDataAdapter da = new SqlDataAdapter(query, myConnectionString);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                rmail = dt.Rows[0]["AssetTypeName"].ToString();
            }
            return rmail;
        }

        public string GetLocation(string loc)
        {
            SqlConnection myConnectionString = new SqlConnection(WebConfigurationManager.ConnectionStrings["VisitorConnectionString"].ConnectionString);
            string rmail = "";
            string query = "SELECT Location FROM LocationMaster where LocationCode = '" + loc + "'";
            SqlDataAdapter da = new SqlDataAdapter(query, myConnectionString);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                rmail = dt.Rows[0]["Location"].ToString();
            }
            return rmail;
        }

        public string GetReportingAuth(string custid)
        {
            SqlConnection myConnectionString = new SqlConnection(WebConfigurationManager.ConnectionStrings["ASSETManagementConnectionString"].ConnectionString);
            string rmail = "";
            string query = "SELECT reporting_staff_no FROM vEmpDtlsAssetApp where CustodianID = '" + custid + "'";
            SqlDataAdapter da = new SqlDataAdapter(query, myConnectionString);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                rmail = dt.Rows[0]["reporting_staff_no"].ToString();
            }
            return rmail;
        }

    }
}
//}
