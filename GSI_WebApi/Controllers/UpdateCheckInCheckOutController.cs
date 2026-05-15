using NRL_WebApi.Models;
using System;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.Http;

namespace NRL_WebApi.Controllers
{
    public class UpdateCheckInCheckOutController : ApiController
    {
        [HttpPost]
        public IHttpActionResult checktimeStatus(Visitors_History data)
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["VisitorConnectionString"].ConnectionString))
            {
                SqlCommand cmd = null;
                string sqlState = "UPDATE VisitorsHistory SET CheckInTime=@CheckInTime,CheckOutTime=@CheckOutTime Where VisitorID='" + data.VisitorID + "'";
                cmd = new SqlCommand(sqlState, conn);
                if (data.CheckOutTime != null)
                {
                    cmd.Parameters.AddWithValue("@CheckInTime", data.CheckInTime);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CheckInTime", data.CheckOutTime);
                }

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                string sqlState1 = "Insert into GatePasshistory (USR_LOGIN,VisitorID, CheckInTime,CheckOutTime) VALUES(@USR_LOGIN,@VisitorID, @CheckInTime, @CheckOutTime)";
                cmd = new SqlCommand(sqlState1, conn);
                cmd.Parameters.AddWithValue("@USR_LOGIN", data.USR_LOGIN);
                cmd.Parameters.AddWithValue("@VisitorID", data.VisitorID);
                if (data.CheckOutTime != null)
                {
                    cmd.Parameters.AddWithValue("@CheckInTime", data.CheckInTime);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CheckInTime", DBNull.Value);
                }
                if (data.CheckOutTime != null)
                {

                    cmd.Parameters.AddWithValue("@CheckOutTime", data.CheckOutTime);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CheckOutTime", DBNull.Value);
                }
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                return Ok();
            }
        }
    }
}
