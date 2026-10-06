namespace DataAccess.Entities
{
    public class VisitorEntryLog
    {
        public int VisitorEntryLogId { get; set; }

        public int VisitorId { get; set; }

        public int GirlId { get; set; }

        public DateTime VisitDate { get; set; }

        public DateTime InTime { get; set; }

        public DateTime OutTime { get; set; }

        public string Purpose { get; set; }

        public Visitor Visitor { get; set; }

        public Girl Girl { get; set; }
    }
}
