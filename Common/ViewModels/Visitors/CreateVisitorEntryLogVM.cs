using System.ComponentModel.DataAnnotations;

namespace Common.ViewModels.Visitors
{
    public class CreateVisitorEntryLogVM
    {
        [Required]
        public int VisitorId { get; set; }

        [Required]
        public int GirlId { get; set; }

        [Required]
        public DateTime VisitDate { get; set; }

        [Required]
        public DateTime InTime { get; set; }

        [Required]
        public string Purpose { get; set; }
    }
}
