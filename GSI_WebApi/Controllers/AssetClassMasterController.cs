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
    public class AssetClassMasterController : ApiController
    {
        [HttpGet]
        public List<AssetClassMasterModel> Get(AssetClassMasterModel data)
        {
            List<AssetClassMasterModel> assetClasses = new List<AssetClassMasterModel>();
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["NRLSAPConnectionString"].ConnectionString))
            {

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "SELECT [AssetClassID], [AssetClassName], [DepartmentCode],DepartmentName FROM [AssetClassMaster]  where Status='Active'";
                    // cmd.CommandText = "Select * from AuditMaster where LocationID=@locationid";
                    // cmd.Parameters.AddWithValue("@locationid", locationid);
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (rdr.Read())
                    {
                        AssetClassMasterModel classes = new AssetClassMasterModel();
                        classes.AssetClassID = Convert.ToInt32(rdr["AssetClassID"]);
                        classes.AssetClassName = Convert.ToString(rdr["AssetClassName"]);
                        classes.DepartmentCode = Convert.ToString(rdr["DepartmentCode"]);
                        classes.DepartmentName = Convert.ToString(rdr["DepartmentName"]);
                        assetClasses.Add(classes);
                    }
                    conn.Close();
                    return assetClasses;
                }
            }
        }
    }
}
