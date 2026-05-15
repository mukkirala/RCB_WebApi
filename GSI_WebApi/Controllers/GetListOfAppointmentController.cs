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
    public class GetListOfAppointmentController : ApiController
    {
        // public List<GetListOfAppointment> Get()
        [HttpPost]
        public List<GetListOfAppointment> Get(GetListOfAppointment data)
        {
            List<GetListOfAppointment> Appointment = new List<GetListOfAppointment>();
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["VisitorConnectionString"].ConnectionString))
            {

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT  AppointmentID, Name, PurposeofVisit, MobileNo, Date FROM AppointmentSchedule WHERE DepartmentID='"+data.DepartmentID+ "' AND Status='Pending' OR DepartmentID='" + data.SecondDepartment + "' AND Status='Pending' ";
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    while (rdr.Read())
                    {
                        GetListOfAppointment Appointid = new GetListOfAppointment();
                        Appointid.AppointmentID = Convert.ToInt32(rdr["AppointmentID"]);
                        Appointid.Name = rdr["Name"].ToString();
                        Appointid.PurposeofVisit = rdr["PurposeofVisit"].ToString();
                        Appointid.MobileNo = rdr["MobileNo"].ToString();
                       // Appointid.Date = Convert.ToDateTime(rdr["Date"].ToString());
                        Appointid.Date = (rdr["Date"] == DBNull.Value) ? (DateTime?)null : ((DateTime)rdr["Date"]);
                        Appointment.Add(Appointid);
                    }
                    conn.Close();
                    return Appointment;
                }
            }
        }

        //[HttpPost]
        //public IHttpActionResult PostStatus(GetListOfAppointment status)
        //{
        //    using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["VisitorConnectionString"].ConnectionString))
        //    {
        //        SqlCommand cmd = null;

        //        string sqlState = "UPDATE AppointmentSchedule SET Status=@Status";
        //        cmd = new SqlCommand(sqlState, conn);
        //        cmd.Parameters.AddWithValue("@Status", status.Status);
        //        conn.Open();
        //        cmd.ExecuteNonQuery();
        //        conn.Close();
        //        return Ok();
        //    }
        //}


    }
}
