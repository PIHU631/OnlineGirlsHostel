using System.ComponentModel.DataAnnotations;

namespace Common.ViewModels.Hostels
{
    public class CreateHostelVM
    {
        [Required(ErrorMessage = "Hostel name is required")]
        [StringLength(150)]
        public string HostelName { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(300)]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required")]
        [StringLength(100)]
        public string City { get; set; }

        [Required(ErrorMessage = "Warden name is required")]
        [StringLength(100)]
        public string WardenName { get; set; }

        [Required(ErrorMessage = "Warden phone is required")]
        [Phone]
        public string WardenPhone { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
