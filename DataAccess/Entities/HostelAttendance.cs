namespace DataAccess.Entities
{
    public class HostelAttendance
    {
        public int HostelAttendanceId { get; set; }

        public int GirlId { get; set; }

        public DateTime Date { get; set; }

        public bool IsPresent { get; set; }

        public Girl Girl { get; set; }
    }
}
