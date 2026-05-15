using NRL_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;

namespace NRL_WebApi.Controllers
{
    public class ViewVehicleHistoryController : ApiController
    {
        public List<GetVehicleHistory> GetData()
        {
            List<GetVehicleHistory> Appointment = new List<GetVehicleHistory>();
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["VisitorConnectionString"].ConnectionString))
            {

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT USR_LOGIN, VehicleNumber, V_CheckIn, V_CheckOut,CheckInKilometerCovered,CheckOutKilometerCovered FROM GatepassVehicleHistory INNER JOIN VehicleRegistration ON VehicleRegistration.VehicleID=GatepassVehicleHistory.VehicleID WHERE GatepassVehicleHistory.VehicleID IN (SELECT  VehicleID from GatepassVehicleHistory where USR_LOGIN='NHPOST' and V_CheckOut is null)";
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    while (rdr.Read())
                    {
                        GetVehicleHistory Appointid = new GetVehicleHistory();
                        Appointid.USR_LOGIN = rdr["USR_LOGIN"].ToString();
                        Appointid.VehicleNumber = rdr["VehicleNumber"].ToString();
                        Appointid.V_CheckIn = Convert.ToDateTime(rdr["V_CheckIn"].ToString());
                        Appointid.CheckInKilometerCovered = rdr["CheckInKilometerCovered"].ToString();
                        Appointid.V_CheckOut = (rdr["V_CheckOut"] == DBNull.Value) ? (DateTime?)null : ((DateTime)rdr["V_CheckOut"]);
                        Appointid.CheckOutKilometerCovered = rdr["CheckOutKilometerCovered"].ToString();
                        Appointment.Add(Appointid);
                    }
                    conn.Close();
                    return Appointment;
                }
            }
        }
    }
}
