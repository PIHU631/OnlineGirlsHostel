namespace DataAccess.Entities
{
    public class Room
    {
        public int RoomId { get; set; }

        public int HostelId { get; set; }

        public string RoomNumber { get; set; }

        public int Floor { get; set; }

        public int Capacity { get; set; } = 3;

        public bool IsActive { get; set; }

        public Hostel Hostel { get; set; }

        public ICollection<Girl> Girls { get; set; }

        public ICollection<RoomHistory> RoomHistories { get; set; }
    }
}
