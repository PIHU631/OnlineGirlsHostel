namespace DataAccess.Entities
{
    public class EntryLog
    {
        public int EntryLogId { get; set; }

        public int GirlId { get; set; }

        public DateTime OutTime { get; set; }

        public DateTime? InTime { get; set; }

        public string Reason { get; set; }

        public Girl Girl { get; set; }
    }
}
