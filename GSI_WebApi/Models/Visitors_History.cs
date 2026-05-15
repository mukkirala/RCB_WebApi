using System;
using System.ComponentModel.DataAnnotations;

namespace NRL_WebApi.Models
{
    public class Visitors_History : GatePassHistory
    {
        public int VisitorID { get; set; }
        public DateTime  CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
       
    }
}