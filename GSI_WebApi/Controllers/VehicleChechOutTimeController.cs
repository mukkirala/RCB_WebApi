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
    public class VehicleChechOutTimeController : ApiController
    {
        [HttpPost]
        public IHttpActionResult VehicleCheckOutStatus(GetVehicleHistory data)
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["VisitorConnectionString"].ConnectionString))
            {
                SqlCommand cmd = null;

                string sqlState1 = "UPDATE GatepassVehicleHistory SET V_CheckOut=@V_CheckOut,CheckOutKilometerCovered=@CheckOutKilometerCovered Where VehicleID='" + data.VehicleID + "' AND USR_LOGIN ='" + data.USR_LOGIN + "' AND V_CheckOut IS NULL";
                cmd = new SqlCommand(sqlState1, conn);
                cmd.Parameters.AddWithValue("@V_CheckOut", DateTime.Now);
                cmd.Parameters.AddWithValue("@CheckOutKilometerCovered", data.CheckOutKilometerCovered);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return Ok();
            }
        }
    }
}
