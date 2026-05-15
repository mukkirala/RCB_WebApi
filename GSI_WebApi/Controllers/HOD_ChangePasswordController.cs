using NRL_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;

namespace NRL_WebApi.Controllers
{
    public class HOD_ChangePasswordController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Changepwd(HOD_DepartmentLogin data)
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["VisitorConnectionString"].ConnectionString))
            {
                SqlCommand cmd = null;
                string value = data.USR_PASSWORD;
                string passed = Utilities.EncryptTripleDES(value);
                string sqlState = "UPDATE tblUSER SET USR_PASSWORD=@USR_PASSWORD Where USR_PHONE_NO='" + data.USR_PHONE_NO + "'";
                cmd = new SqlCommand(sqlState, conn);
                cmd.Parameters.AddWithValue("@USR_PASSWORD", passed);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return Ok();
            }
        }
    }
}
