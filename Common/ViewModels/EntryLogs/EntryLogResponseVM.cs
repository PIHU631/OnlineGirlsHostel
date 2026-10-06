namespace Common.ViewModels.EntryLogs
{
    public class EntryLogResponseVM
    {
        public int EntryLogId { get; set; }

        public int GirlId { get; set; }

        public string GirlName { get; set; }

        public DateTime OutTime { get; set; }

        public DateTime? InTime { get; set; }

        public string Reason { get; set; }

        public bool IsReturned => InTime != null;
    }
}
