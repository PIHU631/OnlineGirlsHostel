using System.ComponentModel.DataAnnotations;

namespace Common.ViewModels.Hostels
{
    public class UpdateHostelVM
    {
        [Required]
        public int HostelId { get; set; }

        [Required]
        [StringLength(150)]
        public string HostelName { get; set; }

        [Required]
        [StringLength(300)]
        public string Address { get; set; }

        [Required]
        [StringLength(100)]
        public string City { get; set; }

        [Required]
        [StringLength(100)]
        public string WardenName { get; set; }

        [Required]
        [Phone]
        public string WardenPhone { get; set; }

        public bool IsActive { get; set; }
    }
}
