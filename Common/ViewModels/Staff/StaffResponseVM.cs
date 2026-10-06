using Common.Enums;

namespace Common.ViewModels.Staff
{
    public class StaffResponseVM
    {
        public int StaffId { get; set; }

        public int HostelId { get; set; }

        public string HostelName { get; set; }

        public string Name { get; set; }

        public StaffRole Role { get; set; }

        public string Phone { get; set; }
    }
}
