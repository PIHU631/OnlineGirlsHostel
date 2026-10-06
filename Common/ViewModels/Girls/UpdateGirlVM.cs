using System.ComponentModel.DataAnnotations;

namespace Common.ViewModels.Girls
{
    public class UpdateGirlVM
    {
        [Required]
        public int GirlId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Phone]
        public string Phone { get; set; }

        [StringLength(100)]
        public string GuardianName { get; set; }

        [Phone]
        public string GuardianPhone { get; set; }

        [Range(1, int.MaxValue)]
        public int RoomId { get; set; }

        public bool IsActive { get; set; }
    }
}
