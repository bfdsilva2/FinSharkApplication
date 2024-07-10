using System.ComponentModel.DataAnnotations;

namespace FinShark.DTOs.Stock
{
    public class CreateStockRequestDto
    {
        [Required]
        [MinLength(1, ErrorMessage = "Symbol must have a minimun of 1 characters")]
        [MaxLength(5, ErrorMessage = "Symbol must have a maximun of 5 characters ")]
        public string Symbol { get; set; } = string.Empty;

        [Required]
        [MaxLength(255, ErrorMessage = "Company Name must have a maximun of 255 characters ")]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        [Range(1,1000000000)]
        public decimal Purchase { get; set; }

        [Required]
        [Range(0.001, 10000)]
        public decimal LastDiv { get; set; }

        [Required]
        [MaxLength(255, ErrorMessage = "Industry Name must have a maximun of 255 characters ")]
        public string Industry { get; set; } = string.Empty;

        [Required]
        [Range(1, 5000000000)]
        public long MarketCap { get; set; }
    }
}
