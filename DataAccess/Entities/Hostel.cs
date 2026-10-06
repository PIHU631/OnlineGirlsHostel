namespace DataAccess.Entities
{
    public class Hostel
    {
        public int HostelId { get; set; }

        public string HostelName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string WardenName { get; set; }

        public string WardenPhone { get; set; }

        public bool IsActive { get; set; }

        public ICollection<Room> Rooms { get; set; } = new List<Room>();

        public ICollection<Staff> Staffs { get; set; } = new List<Staff>();

    }
}
