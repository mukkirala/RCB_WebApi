using NRL_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.Http;
using System.DirectoryServices;
using System.Web;

namespace NRL_WebApi.Controllers
{
    public class UserLoginController : ApiController
    {
   

        [HttpPost]
        public List<UserLogin> Get(UserLogin data)
        {
            
            List<UserLogin> login = new List<UserLogin>();
          

            UserLogin ul = new UserLogin();
          
                string emp = "";

           
             emp = data.USR_LOGIN.ToLower();

            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["VisitorConnectionString"].ConnectionString))
            {

                using (SqlCommand cmd = new SqlCommand())
                {
                    string SelectQery = "SELECT CustodianID,CustodianDepartmentCode,DepartmentName,CustodianName,Designation,reporting_staff_no,email FROM CustodianMaster left join DepartmentMaster on DepartmentMaster.DepartmentCode = CustodianMaster.CustodianDepartmentCode where LDAP_USERID='" + emp + "' and LDAP_PWD='" + Utilities.EncryptTripleDES(data.USR_PASSWORD).ToString() + "'";

                    conn.Open();
                  //  DataTable dt = new DataTable();
                  //  SqlDataAdapter da = new SqlDataAdapter(SelectQery, conn);
                  //  da.Fill(dt);
                  int avl;//= ul.RoleExist(emp, "Auditor");
                  ////  string classid = GetAssetClassID(emp);
                  //  if (dt.Rows.Count <= 0)
                  //  {
                  //      SelectQery = "SELECT CustodianID,CustodianDepartmentCode,DepartmentName,CustodianName,Designation,reporting_staff_no,email FROM CustodianMaster left join DepartmentMaster on DepartmentMaster.DepartmentCode = CustodianMaster.CustodianDepartmentCode where CustodianID='" + data.USR_LOGIN.ToLower() + "'";
                  //      //   avl = ul.RoleExist(data.USR_LOGIN, "Auditor");
                  //     // classid = GetAssetClassID(data.USR_LOGIN);
                  //  }
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = SelectQery;
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (rdr.Read())
                    {
                        UserLogin type = new UserLogin();
                        type.CustodianID = rdr["CustodianID"].ToString();
                        string CustodianDepartmentCode = rdr["CustodianDepartmentCode"].ToString();
                        type.CustodianDepartmentCode = CustodianDepartmentCode;
                        string DepartmentName = rdr["DepartmentName"].ToString();
                        type.DepartmentName = DepartmentName;
                        string CustodianName = rdr["CustodianName"].ToString();
                        type.CustodianName = CustodianName;
                        string Designation = rdr["Designation"].ToString();
                        type.Designation = Designation;


                        if (data.logintype == "0")
                        {
                            avl = ul.RoleExist(type.CustodianID, "Auditor");
                            if (avl == 1)
                            {
                                //string AssetClassID = classid;
                               // type.AssetClassID = AssetClassID;
                                type.USR_ROLEID = "Auditor";
                                type.logintype = "0";
                                login.Add(type);
                            }
                            else
                            {
                                return login;
                            }
                        }
                        else if (data.logintype == "1")
                        {
                            avl = ul.RoleExist(type.CustodianID, "Admin");

                            if (avl == 1)
                            {
                                //string AssetClassID = "";
                                //type.AssetClassID = AssetClassID;
                                type.USR_ROLEID = "Admin";
                                type.logintype="1";
                                login.Add(type);
                            }
                            else
                            {
                                return login;
                            }
                        }
                    }

                    conn.Close();
                    return login;
                }
            }
               //}
            //}
            return login;
        }



        //Get AssetClass id from role master 
        public string GetAssetClassID(string Cust)
        {
            SqlConnection myConnectionString = new SqlConnection(WebConfigurationManager.ConnectionStrings["VisitorConnectionString"].ConnectionString);
            string classid = "";
            string query = "Select AssetClassID from RoleMaster where CustodianID='" + Cust + "' and  ROLE_NAME='Auditor' and  ROLE_STATUS='Active'";
            SqlDataAdapter da = new SqlDataAdapter(query, myConnectionString);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                classid = dt.Rows[0]["AssetClassID"].ToString();
            }
            return classid;
        }


        // LOgin from Neemus server
        //        [HttpPost]
        //public List<UserLogin> Get(UserLogin data)
        ////string checkuserAuthorization(UserLogin data)
        //{
        //    List<UserLogin> login = new List<UserLogin>();
        //    // using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["NRLEMPConnectionString"].ConnectionString))
        //    using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["ASSETManagementConnectionString"].ConnectionString))
        //    {

        //        using (SqlCommand cmd = new SqlCommand())
        //        {
        //            cmd.Connection = conn;
        //            cmd.CommandType = CommandType.Text;

        //            //cmd.CommandText = "SELECT USR_ID, USR_NAME,tblUSER.DESIGNATION,USR_TYPE,USR_ROLEID,tblUSER.EmailId,EmployeeID FROM tblUSER inner join EmployeeMaster on EmployeeMaster.EmailID=tblUSER.EmailId where USR_LOGIN='" + data.USR_LOGIN + "' and USR_PASSWORD='" + Utilities.EncryptTripleDES(data.USR_PASSWORD).ToString() + "'";
        //            cmd.CommandText = "SELECT CustodianID,CustodianDepartmentCode,DepartmentName,CustodianName,Designation FROM vEmpDtlsAssetApp inner join vDepartmentAssetApp on vDepartmentAssetApp.DepartmentCode = vEmpDtlsAssetApp.CustodianDepartmentCode where CustodianID='" + data.CustodianID + "'";

        //            conn.Open();
        //            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        //            while (rdr.Read())
        //            {
        //                UserLogin type = new UserLogin();
        //                type.CustodianID = rdr["CustodianID"].ToString();
        //                string CustodianDepartmentCode = rdr["CustodianDepartmentCode"].ToString();
        //                type.CustodianDepartmentCode = CustodianDepartmentCode;
        //                string DepartmentName = rdr["DepartmentName"].ToString();
        //                type.DepartmentName = DepartmentName;
        //                string CustodianName = rdr["CustodianName"].ToString();
        //                type.CustodianName = CustodianName;
        //                string Designation = rdr["Designation"].ToString();
        //                type.Designation = Designation;

        //                //type.USR_ID = Convert.ToInt32(rdr["USR_ID"]);
        //                //string USR_NAME = rdr["USR_NAME"].ToString();
        //                //type.USR_NAME = USR_NAME;
        //                //string DESIGNATION = rdr["DESIGNATION"].ToString();
        //                //type.DESIGNATION = DESIGNATION;
        //                //string USR_TYPE = rdr["USR_TYPE"].ToString();
        //                //type.USR_TYPE = USR_TYPE;
        //                //string USR_ROLEID = rdr["USR_ROLEID"].ToString();
        //                //type.USR_ROLEID = USR_ROLEID;
        //                //string EmailId = rdr["EmailId"].ToString();
        //                //type.EmailId = EmailId;
        //                //string EmployeeID = rdr["EmployeeID"].ToString();
        //                //type.EmployeeID = EmployeeID;
        //                login.Add(type);
        //            }
        //            conn.Close();
        //            return login;


        //        }
        //    }
        //}
    }
}
