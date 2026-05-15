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
    public class InsertLocTransferRequestController: ApiController
    {
        [HttpPost]
        public string LocTransferReq(LocationChangeRequest data)
        {
            try
            {
                SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["VisitorConnectionString"].ConnectionString);
                SqlCommand cmd = null;
                string statusval = "";
                string authid = GetReportingAuth(data.EmployeeID);
                string sqlState = "insert into EmployeeLocationChange(AssetID,EmployeeID,CustodianComments,ToLocation,Status,Date,RequestBy,CustodianDepartment,CustDepartmentCode,CustDesignation,ApproverID,RequestType)values(@AssetID,@EmployeeID,@CustodianComments,@ToLocation,@Status,@Date,@RequestBy,@CustodianDepartment,@CustDepartmentCode,@CustDesignation,@ApproverID,@RequestType)";
                cmd = new SqlCommand(sqlState, conn);
                cmd.Parameters.AddWithValue("@AssetID", data.AssetID);
               // cmd.Parameters.AddWithValue("@ApproverComments", data.ApproverComments);
                cmd.Parameters.AddWithValue("@ToLocation", data.ToLocation);
                cmd.Parameters.AddWithValue("@EmployeeID", data.EmployeeID);
                cmd.Parameters.AddWithValue("@CustodianComments", data.CustodianComments);
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
                cmd.Parameters.AddWithValue("@RequestBy", data.RequestBy);
                cmd.Parameters.AddWithValue("@CustodianDepartment", data.CustodianDepartment);
                cmd.Parameters.AddWithValue("@CustDepartmentCode", data.CustDepartmentCode);
                cmd.Parameters.AddWithValue("@CustDesignation", data.CustDesignation);
                cmd.Parameters.AddWithValue("@ApproverID", authid);
                cmd.Parameters.AddWithValue("@RequestType", "Location Transfer");
                conn.Open();
                cmd.ExecuteNonQuery();
                string asset = GetAssetType(data.AssetID);
                string main = GetAssetMain(data.AssetID);
                string location = GetLocation(data.ToLocation);
                string subject = "Request For Asset Location Transfer";
                string msg2 = "New Request of Location Transfer from Employee ID <b>" + data.EmployeeID + ", " + data.RequestBy + "</b> for Asset with MainAssetNumber: <b>" + main + "</b> and Asset Description: <b>" + asset + " </b>, is requested to transfer to the location: <b>" + location + "</b>.<br/>" + "<b>Please verify to complete the approval process.</b>";
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

    }
}
