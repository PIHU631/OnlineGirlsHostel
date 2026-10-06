using System.ComponentModel.DataAnnotations;

namespace Common.ViewModels.Rooms
{
    public class UpdateRoomVM
    {
        [Required]
        public int RoomId { get; set; }

        [Required]
        public int HostelId { get; set; }

        [Required]
        public string RoomNumber { get; set; }

        public int Floor { get; set; }

        public int Capacity { get; set; }

        public bool IsActive { get; set; }
    }
}
