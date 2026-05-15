using GSI_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;

namespace GSI_WebApi.Controllers
{
    public class assignAssetController : ApiController
    {
        [HttpPost]
       
        public IHttpActionResult AssignAsset([FromBody] AssetAssignRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid input data.");
            }

            try
            {
                // Fetch required data based on provided identities
                var custodianDepartment = GetCustodianDepartment(request.CustodianId);
                var locationInfo = GetLocationInfo(request.LocationId);

                // Update AssetMaster table
                using (SqlConnection conAMS = new SqlConnection(WebConfigurationManager.ConnectionStrings["NRLSAPConnectionString"].ConnectionString))
                {
                    string updateQuery = "UPDATE AssetMaster SET Status='LTRF', StatusDesc='Asset Location Transferred', CustodianID=@CustodianID, CustodianDepartment=@CustodianDepartment, Location=@Location, LocationDesc=@LocationDesc, LocationID=@LocationID, Block=@Block WHERE AssetID=@AssetID";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, conAMS))
                    {
                        cmd.Parameters.AddWithValue("@CustodianID", request.CustodianId);
                        cmd.Parameters.AddWithValue("@CustodianDepartment", custodianDepartment);
                        cmd.Parameters.AddWithValue("@LocationDesc", locationInfo.LocationCode);
                        cmd.Parameters.AddWithValue("@Location", locationInfo.Location);
                        cmd.Parameters.AddWithValue("@LocationID", request.LocationId);
                        cmd.Parameters.AddWithValue("@Block", locationInfo.Block);
                        cmd.Parameters.AddWithValue("@AssetID", request.AssetId);

                        conAMS.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                return Ok("Asset Master updated successfully");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        private string GetCustodianDepartment(string custodianID)
        {
            using (SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["VisitorConnectionString"].ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(@"SELECT DepartmentName FROM DepartmentMaster
                                    inner join CustodianMaster on DepartmentMaster.DepartmentCode=CustodianMaster.CustodianDepartmentCode
                                    WHERE CustodianID = @CustodianID", con))
                {
                    cmd.Parameters.AddWithValue("@CustodianID", custodianID);
                    return cmd.ExecuteScalar()?.ToString();
                }
            }
        }

        private (string Location, string LocationCode, string Block) GetLocationInfo(int locationID)
        {
            using (SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["VisitorConnectionString"].ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT Location, LocationCode, Block FROM LocationMaster WHERE LocationID = @LocationID", con))
                {
                    cmd.Parameters.AddWithValue("@LocationID", locationID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return (
                                reader["Location"].ToString(),
                                reader["LocationCode"].ToString(),
                                reader["Block"].ToString()
                            );
                        }
                        else
                        {
                            throw new Exception("Location not found");
                        }
                    }
                }
            }
        }


    }
}
