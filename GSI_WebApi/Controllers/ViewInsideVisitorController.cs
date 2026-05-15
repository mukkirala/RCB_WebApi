using NRL_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.Http;

namespace NRL_WebApi.Controllers
{
    public class ViewInsideVisitorController : ApiController
    {
        [HttpGet]
        public List<Visitors_History> Getvistoryhistory()
        {
            List<Visitors_History> visitoryhistory = new List<Visitors_History>();
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["VisitorConnectionString"].ConnectionString))
            {

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT USR_LOGIN, Name, GatePasshistory.CheckInTime, GatePasshistory.CheckOutTime FROM GatePasshistory INNER JOIN VisitorMaster ON VisitorMaster.VisitorID=GatePasshistory.VisitorID WHERE GatePasshistory.VisitorID in(SELECT  GatePasshistory.VisitorID from GatePasshistory where USR_LOGIN='NHPOST' and CheckOutTime is null)";
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    while (rdr.Read())
                    {
                        Visitors_History history = new Visitors_History();
                        history.USR_LOGIN = rdr["USR_LOGIN"].ToString();
                        history.Name = rdr["Name"].ToString();
                        history.CheckInTime = Convert.ToDateTime(rdr["CheckInTime"].ToString());
                        history.CheckOutTime = (rdr["CheckOutTime"] == DBNull.Value) ? (DateTime?)null : ((DateTime)rdr["CheckOutTime"]);
                        // history.CheckOutTime = Convert.ToDateTime(rdr["CheckOutTime"].ToString());
                        visitoryhistory.Add(history);
                    }
                    conn.Close();
                    return visitoryhistory;
                }
            }
        }
    }
}
