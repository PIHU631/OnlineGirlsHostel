namespace Common.ViewModels.RoomHistory
{
    public class RoomHistoryResponseVM
    {
        public int RoomHistoryId { get; set; }

        public int GirlId { get; set; }

        public string GirlName { get; set; }

        public int RoomId { get; set; }

        public string RoomNumber { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsCurrent => EndDate == null;
    }
}
