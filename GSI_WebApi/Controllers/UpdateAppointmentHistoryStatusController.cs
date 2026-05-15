using NRL_WebApi.Models;
using System;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.Http;

namespace NRL_WebApi.Controllers
{
    public class UpdateAppointmentHistoryStatusController : ApiController
    {
        [HttpPost]
        public IHttpActionResult UpdateHistoryStatus(GetAppointmentlistbySystem status)
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["VisitorConnectionString"].ConnectionString))
            {
                SqlCommand cmd = null;
                string sqlState = "UPDATE VisitorsHistory SET Status=@Status Where VisitorHistoryID='" + status.VisitorHistoryID + "'";
                cmd = new SqlCommand(sqlState, conn);
                cmd.Parameters.AddWithValue("@Status", status.Status);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                if (status.Status == "Approved")
                {
                    
                    if (status.PhoneNo != "")
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
                        string sqlinsert = "UPDATE VisitorsHistory SET OTP=@OTP Where VisitorHistoryID='" + status.VisitorHistoryID + "'";
                        cmd = new SqlCommand(sqlinsert, conn);
                        cmd.Parameters.AddWithValue("@OTP", otp);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        sms_send(status.PhoneNo, otp);
                    }
                   
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
    }
}
