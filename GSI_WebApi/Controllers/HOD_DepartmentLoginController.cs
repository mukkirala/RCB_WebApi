using NRL_WebApi.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.Http;
namespace NRL_WebApi.Controllers
{
    public class HOD_DepartmentLoginController : ApiController
    {

        [HttpPost]
        public string checkHODAuthorization(HOD_DepartmentLogin data)
        {
            try
            {
                SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["VisitorConnectionString"].ConnectionString);
                DataTable dt = new DataTable();
                string value = data.USR_PASSWORD;
                string passed = Utilities.EncryptTripleDES(value);
                string sqlState = "SELECT  Department FROM tblUSER WHERE USR_LOGIN='" + data.USR_LOGIN + "' AND USR_PASSWORD = '" + passed + "'";
                SqlCommand cmd = new SqlCommand(sqlState, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                conn.Open();
                da.Fill(dt);
                conn.Close();
                string statusval = "";
                if (dt.Rows.Count > 0)
                {
                    statusval = dt.Rows[0].ItemArray[0].ToString();
                }
                else
                {
                    statusval = "0";
                }
                return statusval;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
