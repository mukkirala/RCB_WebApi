using GSI_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web.Configuration;
using System.Web.Http;

namespace GSI_WebApi.Controllers
{
    public class SaveAuditDetailsController : ApiController
    {
        [HttpPost]
        public IHttpActionResult SaveAuditDetails([FromBody] SaveAuditRequest request)
        {
            if (request.RfidTags == null || request.RfidTags.Count == 0)
            {
                return BadRequest("RFID tags are required.");
            }

            List<AuditDetails> auditDetailsList = new List<AuditDetails>();

            // Connect to the NRLSAP database to get asset details
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["NRLSAPConnectionString"].ConnectionString))
            {
                conn.Open();

                foreach (var rfidTag in request.RfidTags)
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT AssetID, MainAssetNumber FROM AssetMaster WHERE RFIDCardNumber = @RfidTag";
                        cmd.Parameters.AddWithValue("@RfidTag", rfidTag);

                        SqlDataReader rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            AuditDetails auditDetails = new AuditDetails
                            {
                                MainAssetNumber = rdr["MainAssetNumber"].ToString(),
                                AuditStatus = "Asset Available",
                                Comments = "",
                                AuditID = request.AuditID,
                                Date = DateTime.Now,
                                AssetID = Convert.ToInt32(rdr["AssetID"]),
                                Status = "Audited",
                                AdminRemarks = "",
                                CustodianID = "Custodian123",
                                AuditBy = request.custodianID,
                                Location = "Location123",
                                RFIDCardNumber= rfidTag,
                                AssetClassID = 0 // Set your asset class ID
                            };

                            auditDetailsList.Add(auditDetails);
                        }

                        rdr.Close();
                    }
                }

                conn.Close();
            }

            // Connect to the Visitor database to save audit details
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["VisitorConnectionString"].ConnectionString))
            {
                conn.Open();

                foreach (var auditDetails in auditDetailsList)
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"
                            INSERT INTO AuditDetails (MainAssetNumber, AuditStatus, Comments, AuditID, Date, AssetID, Status, AdminRemarks, CustodianID, AuditBy, Location, AssetClassID,RFIDCardNumber)
                            VALUES (@MainAssetNumber, @AuditStatus, @Comments, @AuditID, @Date, @AssetID, @Status, @AdminRemarks, @CustodianID, @AuditBy, @Location, @AssetClassID,@RFIDCardNumber)";

                        cmd.Parameters.AddWithValue("@MainAssetNumber", auditDetails.MainAssetNumber);
                        cmd.Parameters.AddWithValue("@AuditStatus", auditDetails.AuditStatus);
                        cmd.Parameters.AddWithValue("@Comments", DBNull.Value);
                        cmd.Parameters.AddWithValue("@AuditID", auditDetails.AuditID);
                        cmd.Parameters.AddWithValue("@Date", auditDetails.Date);
                        cmd.Parameters.AddWithValue("@AssetID", auditDetails.AssetID);
                        cmd.Parameters.AddWithValue("@Status", auditDetails.Status);
                        cmd.Parameters.AddWithValue("@AdminRemarks", DBNull.Value);
                        cmd.Parameters.AddWithValue("@CustodianID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@AuditBy", auditDetails.AuditBy);
                        cmd.Parameters.AddWithValue("@Location", DBNull.Value);
                        cmd.Parameters.AddWithValue("@AssetClassID", auditDetails.AssetClassID);
                        cmd.Parameters.AddWithValue("@RFIDCardNumber", auditDetails.RFIDCardNumber);

                        cmd.ExecuteNonQuery();
                    }
                }

                conn.Close();
            }

            return Ok("Audit details saved successfully");
        }
    }
}
