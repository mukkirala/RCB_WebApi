using System;

namespace NRL_WebApi.Models
{
    public class GetVehicleHistory
    {
        public int GatepassVehicleHistoryID { get; set; }
        public string USR_LOGIN { get; set; }
        public int VehicleID { get; set; }
        public string VehicleNumber { get; set; }
        public DateTime V_CheckIn { get; set; }
        public DateTime? V_CheckOut { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public string CheckInKilometerCovered { get; set; }
        public string CheckOutKilometerCovered { get; set; }

    }
}