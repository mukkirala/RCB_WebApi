using NRL_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.Http;

namespace NRL_WebApi.Controllers
{
    public class StatusMaster1Controller : ApiController
    {
       
        public List<StatusMaster> Get()
        {
            List<StatusMaster> status = new List<StatusMaster>();
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["VisitorConnectionString"].ConnectionString))
            {

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT [StatusCode],[StatusName] FROM [RCBAMS]..[StatusMaster]";
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    while (rdr.Read())
                    {
                        StatusMaster statusid = new StatusMaster();
                        statusid.StatusID = Convert.ToInt32(rdr["StatusID"]);
                        statusid.StatusCode = rdr["StatusCode"].ToString();
                        statusid.StatusName = rdr["StatusName"].ToString();
                        statusid.Status = rdr["Status"].ToString();
                        status.Add(statusid);
                    }
                    conn.Close();
                    return status;
                }
            }
        }

        //public IHttpActionResult PostNewStudent(StatusMaster data)
        //{
        //    using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["VisitorConnectionString"].ConnectionString))
        //    {
        //        SqlCommand cmd = null;

        //        string sqlState = "Insert into AppointmentSchedule (DepartmentID, Name,MobileNo,Date,Time,Company,Designation, Profession,PurposeofVisit,RegDate,Status,UserID) VALUES(@DepartmentID, @Name, @MobileNo, @Date, @Time, @Company, @Designation, @Profession,@PurposeofVisit,@RegDate,@Status,@UserID)";
        //        cmd = new SqlCommand(sqlState, conn);
        //        cmd.Parameters.AddWithValue("@DepartmentID", data.DepartmentID);
        //        cmd.Parameters.AddWithValue("@Name", data.Name);
        //        cmd.Parameters.AddWithValue("@MobileNo", data.MobileNo);
        //        cmd.Parameters.AddWithValue("@Date", data.Date);
        //        cmd.Parameters.AddWithValue("@Time", data.Time);
        //        cmd.Parameters.AddWithValue("@Company", data.Company);
        //        cmd.Parameters.AddWithValue("@Designation", data.Designation);
        //        cmd.Parameters.AddWithValue("@Profession", data.Profession);               
        //        cmd.Parameters.AddWithValue("@PurposeofVisit", data.PurposeofVisit);
        //        cmd.Parameters.AddWithValue("@RegDate", DateTime.Now);
        //        cmd.Parameters.AddWithValue("@Status", "Pending");
        //        cmd.Parameters.AddWithValue("@UserID", data.UserID);
        //        conn.Open();
        //        cmd.ExecuteNonQuery();
        //        conn.Close();
        //        return Ok();
        //    }
        //}
    }
}
