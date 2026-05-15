using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NRL_WebApi.Models;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;

namespace NRL_WebApi.Controllers
{
    public class GetCommodityNameController : ApiController
    {
        public List<GetCommodityName> Get()
        {
            List<GetCommodityName> commodity = new List<GetCommodityName>();
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["GSIConnectionString"].ConnectionString))
            {
               
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT  distinct CommodityID, Commodity FROM CommodityMaster";
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    while (rdr.Read())
                    {
                        GetCommodityName commod = new GetCommodityName();
                        commod.CommodityID = Convert.ToInt32(rdr["CommodityID"]);
                        commod.Commodity = rdr["Commodity"].ToString();
                        commodity.Add(commod);
                    }
                    conn.Close();
                    return commodity;
                }
            }
        }
    }
}
