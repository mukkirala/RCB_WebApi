using NRL_WebApi.Models;
using System;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.Http;

namespace NRL_WebApi.Controllers
{
    public class CheckInTimeController : ApiController
    {
        [HttpPost]
        public IHttpActionResult CheckOutStatus(Visitors_History data)
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["VisitorConnectionString"].ConnectionString))
            {
                SqlCommand cmd = null;
                string sqlState = "UPDATE VisitorsHistory SET CheckInTime=@CheckInTime Where VisitorID='" + data.VisitorID + "'";
                cmd = new SqlCommand(sqlState, conn);
                cmd.Parameters.AddWithValue("@CheckInTime", DateTime.Now);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                string sqlState1 = "Insert into GatePasshistory (USR_LOGIN,VisitorID, CheckInTime) VALUES(@USR_LOGIN,@VisitorID, @CheckInTime)";
                cmd = new SqlCommand(sqlState1, conn);
                cmd.Parameters.AddWithValue("@USR_LOGIN", data.USR_LOGIN);
                cmd.Parameters.AddWithValue("@VisitorID", data.VisitorID);
                cmd.Parameters.AddWithValue("@CheckInTime", DateTime.Now);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return Ok();
            }
        }
    }
}
