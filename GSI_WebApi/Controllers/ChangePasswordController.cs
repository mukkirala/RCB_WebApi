using GSI_WebApi.Models;
using NRL_WebApi.Models;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.Http;

namespace NRL_WebApi.Controllers
{
    public class ChangePasswordController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Changepwd(InsertAuditData data)
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["VisitorConnectionString"].ConnectionString))
            {
                SqlCommand cmd = null;
                string sqlState = "UPDATE UserRegistration SET Password=@Password Where MobileNo='" + data.MobileNo + "'";
                cmd = new SqlCommand(sqlState, conn);
                cmd.Parameters.AddWithValue("@Password", data.Password);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return Ok();
            }
        }
    }
}
