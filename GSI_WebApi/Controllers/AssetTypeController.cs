 using GSI_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;

namespace GSI_WebApi.Controllers
{
    public class AssetTypeController : ApiController
    {
        [HttpPost]
        public List<AssetType> Get(AssetType data)
        {
            List<AssetType> asesttype = new List<AssetType>();
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["NRLSAPConnectionString"].ConnectionString))
            {

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "Select * from AssetTypeMaster where AssetClassName='" + data.AssetClassName + "'  order by AssetTypeName ASC";
                    // cmd.CommandText = "Select * from AuditMaster where LocationID=@locationid";
                  //   cmd.Parameters.AddWithValue("@AssetClassName", data.AssetClassName);
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (rdr.Read())
                    {
                        AssetType type = new AssetType();
                        type.AssetTypeID = Convert.ToInt32(rdr["AssetTypeID"]);
                        type.AssetTypeName = Convert.ToString(rdr["AssetTypeName"]);
                        type.AssetTypeCode = Convert.ToString(rdr["AssetTypeCode"]);
                        asesttype.Add(type);
                    }
                    conn.Close();
                    return asesttype;
                }
            }
        }
    }
}
