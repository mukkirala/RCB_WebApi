using NRL_WebApi.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.Http;

namespace NRL_WebApi.Controllers
{
    public class HOD_MobileVerificationController : ApiController
    {
        [HttpPost]
        public string checkusermobileno(HOD_DepartmentLogin data)
        {
            try
            {
                SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["VisitorConnectionString"].ConnectionString);
                DataTable dt = new DataTable();
                string sqlState = "SELECT  USR_ID FROM tblUSER WHERE USR_PHONE_NO='" + data.USR_PHONE_NO + "'";
                SqlCommand cmd = new SqlCommand(sqlState, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                conn.Open();
                da.Fill(dt);
                conn.Close();
                string statusval = "";
                if (dt.Rows.Count > 0)
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
                    statusval = otp;
                    sms_send(data.USR_PHONE_NO, otp);
                }
                else
                {
                    statusval = "0";
                }
                return statusval;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void sms_send(string mobileno, string sms)
        {
            var web = new System.Net.WebClient();
            var SMS_User = "NEEMUSTEST";
            var SMS_PWd = "neemus";
            //var SMS_Sender = "NEESCH";
            var url = "http://smslogin.mobi/spanelv2/api.php?username=" + SMS_User + "&password=" + SMS_PWd + "&to=" + mobileno + "& from=NEESCH&message="+" Your Reset Password is :" + sms + "";
            string result = web.DownloadString(url);
        }
    }
}
