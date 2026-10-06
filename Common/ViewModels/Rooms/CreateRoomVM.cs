using System.ComponentModel.DataAnnotations;

namespace Common.ViewModels.Rooms
{
    public class CreateRoomVM
    {
        [Required]
        public int HostelId { get; set; }

        [Required]
        [StringLength(10)]
        public string RoomNumber { get; set; }

        [Required]
        public int Floor { get; set; }

        public int Capacity { get; set; } = 3;

        public bool IsActive { get; set; } = true;
    }
}
