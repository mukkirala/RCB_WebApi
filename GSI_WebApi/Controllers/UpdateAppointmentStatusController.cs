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
    public class UpdateAppointmentStatusController : ApiController
    {
        [HttpPost]
        public IHttpActionResult PostStatus(GetListOfAppointment status)
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["VisitorConnectionString"].ConnectionString))
            {
                SqlCommand cmd = null;
                string sqlState = "UPDATE AppointmentSchedule SET Status=@Status Where AppointmentID='" + status.AppointmentID+"'";
                cmd = new SqlCommand(sqlState, conn);
                cmd.Parameters.AddWithValue("@Status", status.Status);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                if (status.Status == "Approved")
                {
                    int lenthofpass = 6;
                    string allowedchars = "";
                    allowedchars = "1,2,3,4,5,6,7,8,9";
                    char[] sep = { ',' };
                    string[] arr = allowedchars.Split(sep);
                    string passwordstring = "";
                    string temp = "";
                    Random rand = new Random();
                    for (int i = 0; i < lenthofpass; i++)
                    {
                        temp = arr[rand.Next(0, arr.Length)];
                        passwordstring += temp;
                    }
                    string otp = passwordstring;
                    //string sqlinsert = "Insert into UserRegistration (Username,MobileNo, Password) VALUES(@Username,@MobileNo,@Password)";
                    //cmd = new SqlCommand(sqlinsert, conn);
                    //cmd.Parameters.AddWithValue("@Username", status.MobileNo);
                    ////cmd.Parameters.AddWithValue("@MobileNo", data.MobileNo);
                    ////cmd.Parameters.AddWithValue("@Password", data.Password);
                    //conn.Open();
                    //cmd.ExecuteNonQuery();
                    //conn.Close();
                  //  sms_send(status.MobileNo, otp);
                }
                return Ok();
            }
        }


        public void sms_send(string mobileno, string sms)
        {
            var web = new System.Net.WebClient();
            var SMS_User = "NEEMUSTEST";
            var SMS_PWd = "neemus";
           // var SMS_Sender = "NEESCH";
            var url = "http://smslogin.mobi/spanelv2/api.php?username=" + SMS_User + "&password=" + SMS_PWd + "&to=" + mobileno + "& from=NEESCH&message=" + sms + "";
            string result = web.DownloadString(url);
        }



        //[Route("api/Vessel/GetOTP")]
        //[HttpGet]
        //public int OTP(string MobileNo)
        //{


        //    int lenthofpass = 4;
        //    string allowedChars = "";
        //    allowedChars = "1,2,3,4,5,6,7,8,9";
        //    char[] sep = { ',' };
        //    string[] arr = allowedChars.Split(sep);
        //    string passwordString = "";
        //    string temp = "";
        //    Random rand = new Random();
        //    for (int i = 0; i < lenthofpass; i++)
        //    {
        //        temp = arr[rand.Next(0, arr.Length)];
        //        passwordString += temp;
        //    }
        //    string OTP = passwordString;
        //    sms_send(MobileNo, OTP);
        //    return Convert.ToInt32(OTP);
        //}
    }
}
