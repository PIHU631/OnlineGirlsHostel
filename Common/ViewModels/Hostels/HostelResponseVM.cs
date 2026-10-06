namespace Common.ViewModels.Hostels
{
    public class HostelResponseVM
    {
        public int HostelId { get; set; }

        public string HostelName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string WardenName { get; set; }

        public string WardenPhone { get; set; }

        public bool IsActive { get; set; }
    }
}
