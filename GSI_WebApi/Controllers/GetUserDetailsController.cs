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
    public class GetUserDetailsController : ApiController
    {
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
                    cmd.CommandText = "SELECT  AppointmentID, Name, PurposeofVisit, MobileNo  FROM AppointmentSchedule WHERE AppointmentID='" + data.AppointmentID+"'";
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
                        Appointment.Add(Appointid);
                    }
                    conn.Close();
                    return Appointment;
                }
            }
        }



        
        //public List<GetListOfAppointment> GetData()
        //{
        //    List<GetListOfAppointment> Appointment = new List<GetListOfAppointment>();
        //    using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["VisitorConnectionString"].ConnectionString))
        //    {

        //        using (SqlCommand cmd = new SqlCommand())
        //        {
        //            cmd.Connection = conn;
        //            cmd.CommandType = CommandType.Text;
        //            cmd.CommandText = "SELECT  AppointmentID, Name, PurposeofVisit, MobileNo, HostName FROM AppointmentSchedule";
        //            conn.Open();
        //            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        //            while (rdr.Read())
        //            {
        //                GetListOfAppointment Appointid = new GetListOfAppointment();
        //                Appointid.AppointmentID = Convert.ToInt32(rdr["AppointmentID"]);
        //                Appointid.Name = rdr["Name"].ToString();
        //                Appointid.PurposeofVisit = rdr["PurposeofVisit"].ToString();
        //                Appointid.MobileNo = rdr["MobileNo"].ToString();
        //                Appointid.HostName = rdr["HostName"].ToString();
        //                Appointment.Add(Appointid);
        //            }
        //            conn.Close();
        //            return Appointment;
        //        }
        //    }
        //}
    }
}
