using System.ComponentModel.DataAnnotations;

namespace Common.ViewModels.MarketVisits
{
    public class CreateMarketVisitVM
    {
        [Required(ErrorMessage = "GirlId is required")]
        [Range(1, int.MaxValue)]
        public int GirlId { get; set; }

        [Required(ErrorMessage = "OutTime is required")]
        public DateTime OutTime { get; set; }

        [Required(ErrorMessage = "Reason is required")]
        [StringLength(250)]
        public string Reason { get; set; }
    }
}