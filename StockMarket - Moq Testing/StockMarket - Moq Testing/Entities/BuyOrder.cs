using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class BuyOrder
    {
        [Key]
        public Guid BuyOrderId { get; set; }
        public string StockSymbol { get; set; }

        [Required(ErrorMessage = "Stock Name can't be null or empty")]
        public string StockName { get; set; }
        public DateTime DateAndTimeOfOrder { get; set; }

        [Range(1, 100000, ErrorMessage = "Value should be between 1 and 100000")]
        public uint Quantity { get; set; }
        [Range(1, 10000, ErrorMessage = "Value should be between 1 and 100000")]
        public double Price { get; set; }
    }
}
