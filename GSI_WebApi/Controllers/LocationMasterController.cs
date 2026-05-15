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
    public class LocationMasterController : ApiController
    {
        public List<LocationMaster> Get()
        {
            List<LocationMaster> location = new List<LocationMaster>();
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["VisitorConnectionString"].ConnectionString))
            {

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "Select * from LocationMaster order by LocationID ASC";
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    while (rdr.Read())
                    {
                        LocationMaster locationid = new LocationMaster();
                        locationid.LocationID = Convert.ToInt32(rdr["LocationID"]);
                        locationid.LocationCode = rdr["LocationCode"].ToString();
                        locationid.Location = rdr["Location"].ToString();
                        locationid.Block = rdr["Block"].ToString();
                        locationid.Status = rdr["Status"].ToString();
                        location.Add(locationid);
                    }
                    conn.Close();
                    return location;
                }
            }
        }

        //public List<LocationMaster> Get()
        //{
        //    List<LocationMaster> location = new List<LocationMaster>();
        //    using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["NRLAMSConnectionString"].ConnectionString))
        //    {

        //        using (SqlCommand cmd = new SqlCommand())
        //        {
        //            cmd.Connection = conn;
        //            cmd.CommandType = CommandType.Text;

        //            cmd.CommandText = "Select * from LocationMaster";
        //            conn.Open();
        //            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        //            while (rdr.Read())
        //            {
        //                LocationMaster locationid = new LocationMaster();
        //                locationid.LocationID = Convert.ToInt32(rdr["LocationID"]);
        //                locationid.LocationCode = rdr["LocationCode"].ToString();
        //                locationid.Location = rdr["Location"].ToString();
        //                locationid.Status = rdr["Status"].ToString();
        //                location.Add(locationid);
        //            }
        //            conn.Close();
        //            return location;
        //        }
        //    }
        //}
    }
}
