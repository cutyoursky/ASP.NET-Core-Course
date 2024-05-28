using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class BuyOrder
    {
        public Guid BuyOrderId { get; set; }
        public string StockSymbol { get; set; }
        public string StockName { get; set;}
        public DateTime DateAndTimeOfOrder { get; set; }

        [Range(1, 100000, ErrorMessage = "Value should be between 1 and 100000")]
        public uint Quantity { get; set; }
        [Range(1, 100000, ErrorMessage = "Value should be between 1 and 100000")]
        public double Price { get; set; }
    }
}
