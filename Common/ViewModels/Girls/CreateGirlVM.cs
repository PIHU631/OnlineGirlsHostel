using System.ComponentModel.DataAnnotations;

namespace Common.ViewModels.Girls
{
    public class CreateGirlVM
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Guardian name is required")]
        [StringLength(100)]
        public string GuardianName { get; set; }

        [Required(ErrorMessage = "Guardian phone is required")]
        [Phone]
        public string GuardianPhone { get; set; }

        [Required(ErrorMessage = "RoomId is required")]
        [Range(1, int.MaxValue)]
        public int RoomId { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
