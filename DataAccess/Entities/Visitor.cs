namespace DataAccess.Entities
{
    public class Visitor
    {
        public int VisitorId { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Relation { get; set; }

        public string IdProofNumber { get; set; }

        public ICollection<VisitorEntryLog> VisitorEntryLogs { get; set; }
    }
}
