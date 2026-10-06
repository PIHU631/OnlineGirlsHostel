namespace Common.ViewModels.HostelAttendances
{
    public class HostelAttendanceResponseVM
    {
        public int HostelAttendanceId { get; set; }

        public int GirlId { get; set; }

        public string GirlName { get; set; }

        public DateTime Date { get; set; }

        public bool IsPresent { get; set; }

        public string Status => IsPresent ? "Present" : "Absent";
    }
}
