using NRL_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.Http;

namespace NRL_WebApi.Controllers
{
    public class ViewInQRCodeController : ApiController
    {
        [HttpPost]
        public List<ViewInQRCode> Get(ViewInQRCode data)
        {
            List<ViewInQRCode> Appointment = new List<ViewInQRCode>();
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["VisitorConnectionString"].ConnectionString))
            {

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT VisitorHistoryID, VisitorMaster.[VisitorID], [Name], [CompanyName], [PhoneNo],PurposeOfVisit,[DateTime], OrganizationName,Duration,Image,NoOfVisitors FROM [VisitorMaster] INNER JOIN VisitorsHistory ON VisitorMaster.VisitorID=VisitorsHistory.VisitorID INNER JOIN DepartmentMaster ON DepartmentMaster.OrganizationID=VisitorsHistory.OrganizationID where VisitorHistoryID='40'";
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    while (rdr.Read())
                    {
                        ViewInQRCode Appointid = new ViewInQRCode();
                        Appointid.VisitorHistoryID = Convert.ToInt32(rdr["VisitorHistoryID"]);
                        Appointid.VisitorID = Convert.ToInt32(rdr["VisitorID"]);
                        Appointid.Name = rdr["Name"].ToString();
                        Appointid.CompanyName = rdr["CompanyName"].ToString();
                        Appointid.PhoneNo = rdr["PhoneNo"].ToString();
                        Appointid.PurposeOfVisit = rdr["PurposeOfVisit"].ToString();
                        Appointid.DateTime = Convert.ToDateTime(rdr["DateTime"].ToString());
                        Appointid.Image = rdr["Image"].ToString();
                        Appointid.OrganizationName = rdr["OrganizationName"].ToString();
                        Appointid.Duration = rdr["Duration"].ToString();
                        Appointid.NoOfVisitors = rdr["NoOfVisitors"].ToString();
                        Appointment.Add(Appointid);
                    }
                    conn.Close();
                    return Appointment;
                }
            }
        }
    }
}
