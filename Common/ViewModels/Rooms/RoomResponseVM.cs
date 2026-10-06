namespace Common.ViewModels.Rooms
{
    public class RoomResponseVM
    {
        public int RoomId { get; set; }

        public int HostelId { get; set; }

        public string HostelName { get; set; }

        public string RoomNumber { get; set; }

        public int Floor { get; set; }

        public int Capacity { get; set; }

        public int Occupancy { get; set; }

        public bool IsActive { get; set; }

        public int AvailableBeds => Capacity - Occupancy;
    }
}
