using NRL_WebApi.Models;
using System;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.Http;

namespace NRL_WebApi.Controllers
{
    public class VehicleChechInTimeController : ApiController
    {
        [HttpPost]
        public IHttpActionResult VehicleCheckOutStatus(GetVehicleHistory data)
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["VisitorConnectionString"].ConnectionString))
            {
                SqlCommand cmd = null;
               
                string sqlState1 = "Insert into GatepassVehicleHistory (USR_LOGIN, VehicleID,V_CheckIn, CheckInKilometerCovered) VALUES(@VehicleID,@V_CheckIn,@CheckInKilometerCovered)";
                cmd = new SqlCommand(sqlState1, conn);
                cmd.Parameters.AddWithValue("@USR_LOGIN", data.USR_LOGIN);
                cmd.Parameters.AddWithValue("@VehicleID", data.VehicleID);
                cmd.Parameters.AddWithValue("@V_CheckIn", DateTime.Now);
                cmd.Parameters.AddWithValue("@CheckInKilometerCovered", data.CheckInKilometerCovered);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return Ok();
            }
        }
    }
}
