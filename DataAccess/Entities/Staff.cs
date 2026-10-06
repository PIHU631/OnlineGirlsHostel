using Common.Enums;

namespace DataAccess.Entities
{
    public class Staff
    {
        public int StaffId { get; set; }

        public int HostelId { get; set; }

        public string Name { get; set; }

        public StaffRole Role { get; set; }

        public string Phone { get; set; }

        public Hostel Hostel { get; set; }


    }
}
