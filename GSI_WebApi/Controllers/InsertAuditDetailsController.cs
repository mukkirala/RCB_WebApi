using GSI_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;

namespace GSI_WebApi.Controllers
{
    public class InsertAuditDetailsController : ApiController
    {
        [HttpPost]
        public IHttpActionResult PostNewStudent(InsertAuditData data)
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["VisitorConnectionString"].ConnectionString))
            {
                //SqlCommand cmd = null;

                //string sqlState = "insert into [AuditDetails](MainAssetNumber,Status,Comments,AuditID,Date)values(@MainAssetNumber,@Status,@Comments,@AuditID,@Date))";
                //cmd = new SqlCommand(sqlState, conn);
                //cmd.Parameters.AddWithValue("@MainAssetNumber", data.MainAssetNumber);
                //cmd.Parameters.AddWithValue("@Status", data.Status);
                //cmd.Parameters.AddWithValue("@Comments", data.Comments);
                //cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                //cmd.Parameters.AddWithValue("@AuditID", data.AuditID);
                //conn.Open();
                //cmd.ExecuteNonQuery();
                //conn.Close();
                return Ok();
            }
        }

    }
}