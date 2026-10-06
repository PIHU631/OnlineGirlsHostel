namespace Common.ViewModels.Visitors
{
    public class VisitorEntryLogResponseVM
    {
        public int VisitorEntryLogId { get; set; }

        public int VisitorId { get; set; }

        public string VisitorName { get; set; }

        public int GirlId { get; set; }

        public string GirlName { get; set; }

        public DateTime VisitDate { get; set; }

        public DateTime InTime { get; set; }

        public DateTime? OutTime { get; set; }

        public string Purpose { get; set; }

        public bool IsInside => OutTime == null;
    }
}
