using NRLWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.Cors;

namespace NRLWebApi.Controllers
    {
   [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-My-Header")] 
    public class InsertScanQRDataController : ApiController
        {
        //[EnableCors(origins: "http://NRLWebApi.neemus.com", headers: "*", methods: "*")]
        //  [HttpPost]
        //  static void  InsertScanData(IEnumerable<ScanQRHistoryModel> scanQRData)
        ////public void InsertScanData([FromBody]IEnumerable<ScanQRHistoryModel> scanQRData)
        //    {
        //    using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["INSEKSILAConnectionString"].ConnectionString))
        //        foreach (var qrdata in scanQRData)
        //            {
        //            SqlCommand cmd = null;
        //            string sqlState = "Insert into ScanHistory (InstrumentID, Scannedby,Scandate_time,InstrumentStatus) VALUES(@InstrumentID, @Scannedby, @Scandate_time, @InstrumentStatus)";
        //            cmd = new SqlCommand(sqlState, conn);
        //            cmd.Parameters.AddWithValue("@InstrumentID", qrdata.InstrumentID);
        //            cmd.Parameters.AddWithValue("@Scannedby", qrdata.Scannedby);
        //            cmd.Parameters.AddWithValue("@Scandate_time", qrdata.Scandate_time);
        //            cmd.Parameters.AddWithValue("@InstrumentStatus", qrdata.InstrumentStatus);
        //            conn.Open();
        //           int result= cmd.ExecuteNonQuery();
        //            conn.Close();
        //            return ;
        //            }
        //    }

        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public  bool  InsertScanData(IEnumerable<ScanQRHistoryModel> data)
            {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["NRLAMSConnectionString"].ConnectionString))
                {
                SqlCommand cmd = null;
                string sqlState1 = "Insert into AuditDetails (MainAssetNumber,Status,Comments,AuditID,Date) VALUES(@MainAssetNumber,@Status,@Comments,@AuditID,@Date)";
                cmd = new SqlCommand(sqlState1, conn);
                foreach (var qrdata in data)
                    {
                    cmd.Parameters.Clear();
                   
                    cmd.Parameters.AddWithValue("@MainAssetNumber", qrdata.MainAssetNumber);
                    cmd.Parameters.AddWithValue("@Status", qrdata.Status);
                    cmd.Parameters.AddWithValue("@Comments", qrdata.Comments);
                    cmd.Parameters.AddWithValue("@AuditID", qrdata.AuditID);
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                    conn.Open();
                    int x= cmd.ExecuteNonQuery();
                    conn.Close();
                    
                    }
                }
            return true;
            }
        }
    }