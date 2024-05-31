using Entities;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class SellOrderRequest : IValidatableObject
    {
        [Required(ErrorMessage = "Stock Symbol can't be empty")]
        public string StockSymbol { get; set; }
    
        [Required(ErrorMessage = "Stock Name can't be empty")]
        public string StockName { get; set; }
    
        public DateTime DateAndTimeOfOrder { get; set; }
    
        [Range(1, 100000, ErrorMessage = "Value should be between 1 and 100000")]
        public uint Quantity { get; set; }
    
        [Range(1, 100000, ErrorMessage = "Value should be between 1 and 100000")]
        public double Price { get; set; }
    
        public SellOrder ToSellOrder()
        {
            return new SellOrder() { StockSymbol = StockSymbol, StockName = StockName, DateAndTimeOfOrder = DateAndTimeOfOrder, Quantity = Quantity, Price = Price };
        }
    
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> results = new List<ValidationResult>();
    
            if (DateAndTimeOfOrder < Convert.ToDateTime("2000-01-01"))
            {
                results.Add(new ValidationResult("Date of the order can't be older than 01.01.2000."));
            }
    
            return results;
        }
    }
}


