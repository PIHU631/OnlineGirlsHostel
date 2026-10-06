using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Girl
    {
        public int GirlId { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string GuardianName { get; set; }

        public string GuardianPhone { get; set; }

        public DateTime AdmissionDate { get; set; }

        public DateTime? LeavingDate { get; set; }

        public bool IsActive { get; set; }

        public int RoomId { get; set; }

        public Room Room { get; set; }

        public ICollection<RoomHistory> RoomHistories { get; set; }

        public ICollection<FoodAttendance> FoodAttendances { get; set; }

        public ICollection<EntryLog> EntryLogs { get; set; }

        public ICollection<MarketVisit> MarketVisits { get; set; }

        public ICollection<HostelAttendance> HostelAttendances { get; set; }

        public ICollection<VisitorEntryLog> VisitorEntryLogs { get; set; }
    }
}
