using System.ComponentModel.DataAnnotations;

namespace Common.ViewModels.EntryLogs
{
    public class CreateEntryLogVM
    {
        [Required]
        public int GirlId { get; set; }

        [Required]
        public DateTime OutTime { get; set; }

        [Required]
        [StringLength(200)]
        public string Reason { get; set; }
    }
}
