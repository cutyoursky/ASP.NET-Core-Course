using Entities;
using ServiceContracts.DTO.ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class SellOrderResponse
    {
        public Guid SellOrderID { get; set; }
        public string StockSymbol { get; set; }
        public string StockName { get; set; }
        public DateTime DateAndTimeOfOrder { get; set; }
        public uint Quantity { get; set; }
        public double Price { get; set; }
        public double TradeAmount { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is not SellOrderResponse) return false;

            SellOrderResponse other = (SellOrderResponse)obj;
            return SellOrderID == other.SellOrderID && StockSymbol == other.StockSymbol && StockName == other.StockName && DateAndTimeOfOrder == other.DateAndTimeOfOrder && Quantity == other.Quantity && Price == other.Price;
        }

        /// <summary>
        /// Returns an int value that represents unique stock id of the current object
        /// </summary>
        /// <returns>unique int value</returns>
        public override int GetHashCode()
        {
            return StockSymbol.GetHashCode();
        }

        /// <summary>
        /// Converts the current object into string which includes the values of all properties
        /// </summary>
        /// <returns>A string with values of all properties of current object</returns>
        public override string ToString()
        {
            return $"Sell Order ID: {SellOrderID}, Stock Symbol: {StockSymbol}, Stock Name: {StockName}, Date and Time of Sell Order: {DateAndTimeOfOrder.ToString("dd MMM yyyy hh:mm ss tt")}, Quantity: {Quantity}, Sell Price: {Price}, Trade Amount: {TradeAmount}";
        }
    }

    public static class SellOrderExtensions
    {
        public static SellOrderResponse ToSellOrderResponse(this SellOrder sellOrder)
        {
            return new SellOrderResponse() { SellOrderID = sellOrder.sellOrderID, StockSymbol = sellOrder.StockSymbol, StockName = sellOrder.StockName, Price = sellOrder.Price, DateAndTimeOfOrder = sellOrder.DateAndTimeOfOrder, Quantity = sellOrder.Quantity, TradeAmount = sellOrder.Price * sellOrder.Quantity };
        }
    }
}
