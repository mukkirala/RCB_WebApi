using GSI_WebApi.Models;
using System;
using System.Data.SqlClient;
using System.Net;
using System.Web.Configuration;
using System.Web.Http;

namespace GSI_WebApi.Controllers
{
    public class mapAssetController : ApiController
    {
        [HttpPost]


        public IHttpActionResult mapAsset(AssetPositions data)
        {
            if (data == null || data.AssetID <= 0 || string.IsNullOrEmpty(data.rfidTag))
            {
                return BadRequest("Invalid input data.");
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(
                    WebConfigurationManager.ConnectionStrings["NRLSAPConnectionString"].ConnectionString))
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        // 1️⃣ If the RFID is already linked to a different asset → remove it
                        string clearOldAssetRFID = @"
                    UPDATE AssetMaster 
                    SET RFIDCardNumber = NULL, RFIDMAPDATE = NULL
                    WHERE RFIDCardNumber = @RFIDCardNumber 
                    AND NOT (AssetID = @AssetID AND SLNO = @SLNO)";

                        using (SqlCommand clearCmd = new SqlCommand(clearOldAssetRFID, conn, transaction))
                        {
                            clearCmd.Parameters.AddWithValue("@RFIDCardNumber", data.rfidTag);
                            clearCmd.Parameters.AddWithValue("@AssetID", data.AssetID);
                            clearCmd.Parameters.AddWithValue("@SLNO", data.slno);
                            clearCmd.ExecuteNonQuery();
                        }

                        // 2️⃣ Check in history table if RFID already exists as Active → make Inactive
                        string checkRFID = @"
                    SELECT COUNT(*) FROM RFIDMappingHistory 
                    WHERE (RFIDCardNumber = @RFID OR SRNO = @SRNO) AND RFIDStatus = 'Active'";

                        using (SqlCommand checkCmd = new SqlCommand(checkRFID, conn, transaction))
                        {
                            checkCmd.Parameters.AddWithValue("@RFID", data.rfidTag);
                            checkCmd.Parameters.AddWithValue("@SRNO", data.slno);
                            int exists = (int)checkCmd.ExecuteScalar();

                            if (exists > 0)
                            {
                                string inactiveQuery = @"
                            UPDATE RFIDMappingHistory
                            SET RFIDStatus = 'Inactive'
                            WHERE (RFIDCardNumber = @RFID OR SRNO = @SRNO) AND RFIDStatus = 'Active'";

                                using (SqlCommand inactiveCmd = new SqlCommand(inactiveQuery, conn, transaction))
                                {
                                    inactiveCmd.Parameters.AddWithValue("@RFID", data.rfidTag);
                                    inactiveCmd.Parameters.AddWithValue("@SRNO", data.slno);
                                    inactiveCmd.ExecuteNonQuery();
                                }
                            }
                        }

                        // 3️⃣ Insert NEW history record as Active
                        string insertHistory = @"
                    INSERT INTO RFIDMappingHistory (RFIDCardNumber, SRNO, RFIDHistoryDate, RFIDStatus)
                    VALUES (@RFIDCardNumber, @SRNO, @RFIDHistoryDate, 'Active')";

                        using (SqlCommand insertCmd = new SqlCommand(insertHistory, conn, transaction))
                        {
                            insertCmd.Parameters.AddWithValue("@RFIDCardNumber", data.rfidTag);
                            insertCmd.Parameters.AddWithValue("@SRNO", data.slno);
                            insertCmd.Parameters.AddWithValue("@RFIDHistoryDate", DateTime.Now.ToString("yyyy-MM-dd"));
                            insertCmd.ExecuteNonQuery();
                        }

                        // 4️⃣ Update the selected asset with new RFID
                        string updateAsset = @"
                    UPDATE AssetMaster 
                    SET RFIDCardNumber = @RFIDCardNumber, RFIDMAPDATE = @RFIDMAPDATE
                    WHERE AssetID = @AssetID AND SLNO = @SLNO";

                        using (SqlCommand cmd = new SqlCommand(updateAsset, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@RFIDCardNumber", data.rfidTag);
                            cmd.Parameters.AddWithValue("@RFIDMAPDATE", DateTime.Now);
                            cmd.Parameters.AddWithValue("@AssetID", data.AssetID);
                            cmd.Parameters.AddWithValue("@SLNO", data.slno);
                            cmd.ExecuteNonQuery();
                        }

                        // 5️⃣ Commit all changes
                        transaction.Commit();
                        return Ok("1");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return InternalServerError(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


    }
}
