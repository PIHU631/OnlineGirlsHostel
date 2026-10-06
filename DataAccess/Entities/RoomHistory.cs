namespace DataAccess.Entities
{
    public class RoomHistory
    {
        public int RoomHistoryId { get; set; }

        public int GirlId { get; set; }

        public int RoomId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public Girl Girl { get; set; }

        public Room Room { get; set; }
    }
}
