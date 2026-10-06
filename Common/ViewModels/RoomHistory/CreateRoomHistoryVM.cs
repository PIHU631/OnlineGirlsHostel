using System.ComponentModel.DataAnnotations;

namespace Common.ViewModels.RoomHistory
{
    public class CreateRoomHistoryVM
    {
        [Required]
        public int GirlId { get; set; }

        [Required]
        public int RoomId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
    }
}