using NRL_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.Http;

namespace NRL_WebApi.Controllers
{
    public class SystemAppointApprovalController : ApiController
    {
        [HttpPost]
        public List<GetAppointmentlistbySystem> Get(GetAppointmentlistbySystem data)
        {
            List<GetAppointmentlistbySystem> Appointment = new List<GetAppointmentlistbySystem>();
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["VisitorConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT VisitorHistoryID, VisitorMaster.[VisitorID], [Name], [CompanyName], [PhoneNo],PurposeOfVisit,[DateTime],VisitorsHistory.[Status],OrganizationID FROM [VisitorMaster] INNER JOIN VisitorsHistory ON VisitorMaster.VisitorID=VisitorsHistory.VisitorID where  VisitorsHistory.[Status] in('Pending') AND VisitorHistoryID='" + data.VisitorHistoryID + "'";
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    while (rdr.Read())
                    {
                        GetAppointmentlistbySystem Appointid = new GetAppointmentlistbySystem();
                        Appointid.VisitorHistoryID = Convert.ToInt32(rdr["VisitorHistoryID"]);
                        Appointid.VisitorID = Convert.ToInt32(rdr["VisitorID"]);
                        Appointid.Name = rdr["Name"].ToString();
                        Appointid.CompanyName = rdr["CompanyName"].ToString();
                        Appointid.PhoneNo = rdr["PhoneNo"].ToString();
                        Appointid.PurposeOfVisit = rdr["PurposeOfVisit"].ToString();
                        Appointid.DateTime = Convert.ToDateTime(rdr["DateTime"].ToString());
                        Appointment.Add(Appointid);
                    }
                    conn.Close();
                    return Appointment;
                }
            }
        }
    }
}
