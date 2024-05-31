using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class SellOrder
    {
        [Key]
        public Guid SellOrderID { get; set; }
        public string StockSymbol { get; set; }

        [Required(ErrorMessage = "Stock Name can't be null or empty")]
        public string StockName { get; set; }
        public DateTime DateAndTimeOfOrder { get; set; }

        [Range(1, 100000, ErrorMessage = "Value should be between 1 and 100000")]
        public uint Quantity { get; set; }

        [Range(1, 100000, ErrorMessage = "Value should be between 1 and 100000")]
        public double Price { get; set; }
    }
}
