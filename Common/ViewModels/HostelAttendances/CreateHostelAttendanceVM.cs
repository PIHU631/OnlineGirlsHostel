using System.ComponentModel.DataAnnotations;

namespace Common.ViewModels.HostelAttendances
{
    public class CreateHostelAttendanceVM
    {
        [Required(ErrorMessage = "GirlId is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid GirlId")]
        public int GirlId { get; set; }

        [Required(ErrorMessage = "Date is required")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Attendance status is required")]
        public bool IsPresent { get; set; }
    }
}
