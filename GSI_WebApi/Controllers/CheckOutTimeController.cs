using NRL_WebApi.Models;
using System;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.Http;

namespace NRL_WebApi.Controllers
{
    public class CheckOutTimeController : ApiController
    {

        [HttpPost]
        public int CheckOutStatus(Visitors_History data)
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

                string sqlState1 = "UPDATE GatePasshistory SET CheckOutTime=@CheckOutTime Where VisitorID='" + data.VisitorID + "' AND USR_LOGIN ='" + data.USR_LOGIN + "' AND CheckOutTime IS NULL";
                cmd = new SqlCommand(sqlState1, conn);
                cmd.Parameters.AddWithValue("@CheckOutTime", DateTime.Now);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return 0;
            }
        }
    }
}
