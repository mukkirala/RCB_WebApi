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
    public class AuditMasterController : ApiController
    {
        [HttpPost]
        public List<AuditMaster> Get(AuditMaster data)
        {
            List<AuditMaster> audit = new List<AuditMaster>();
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["VisitorConnectionString"].ConnectionString))
            {

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                   
                        cmd.CommandText = "Select * from AuditMaster WHERE LocationID='" + data.LocationID + "' and AuditStatus = 'Active' order by AuditName ASC";
                    
                   
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    while (rdr.Read())
                    {
                        AuditMaster auditid = new AuditMaster();
                        auditid.AuditID = Convert.ToInt32(rdr["AuditID"]);
                        auditid.AuditName = Convert.ToString(rdr["AuditName"]);
                        auditid.AuditDate = Convert.ToString(rdr["AuditDate"]);
                        auditid.AuditDescription = Convert.ToString(rdr["AuditDescription"]);
                        auditid.AuditBy = Convert.ToString(rdr["AuditBy"]);
                        auditid.Status = Convert.ToString(rdr["Status"]);
                        auditid.LocationID = Convert.ToInt32(rdr["LocationID"].ToString());
                        audit.Add(auditid);
                    }
                    conn.Close();
                    return audit;
                }
            }
        }


        //public List<AuditMaster> Get()
        //{
        //    List<AuditMaster> audit = new List<AuditMaster>();
        //    using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["NRLAMSConnectionString"].ConnectionString))
        //    {

        //        using (SqlCommand cmd = new SqlCommand())
        //        {
        //            cmd.Connection = conn;
        //            cmd.CommandType = CommandType.Text;

        //            cmd.CommandText = "Select * from AuditMaster";
        //            // cmd.CommandText = "Select * from AuditMaster where LocationID=@locationid";
        //            // cmd.Parameters.AddWithValue("@locationid", locationid);
        //            conn.Open();

        //            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        //            while (rdr.Read())
        //            {
        //                AuditMaster auditid = new AuditMaster();
        //                auditid.AuditID = Convert.ToInt32(rdr["AuditID"]);
        //                auditid.AuditName = Convert.ToString(rdr["AuditName"]);
        //                auditid.AuditDate = Convert.ToString(rdr["AuditDate"]);
        //                auditid.AuditDescription = Convert.ToString(rdr["AuditDescription"]);
        //                auditid.AuditBy = Convert.ToString(rdr["AuditBy"]);
        //                auditid.Status = Convert.ToString(rdr["Status"]);
        //                auditid.LocationID = Convert.ToInt32(rdr["LocationID"].ToString());
        //                audit.Add(auditid);
        //            }
        //            conn.Close();
        //            return audit;
        //        }
        //    }
        //}
    }
}
