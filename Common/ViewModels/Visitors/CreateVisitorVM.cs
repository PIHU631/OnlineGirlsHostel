using System.ComponentModel.DataAnnotations;

namespace Common.ViewModels.Visitors
{
    public class CreateVisitorVM
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        [StringLength(50)]
        public string Relation { get; set; }

        [Required]
        [StringLength(100)]
        public string IdProofNumber { get; set; }
    }
}
