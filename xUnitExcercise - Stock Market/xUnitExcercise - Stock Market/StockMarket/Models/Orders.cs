using ServiceContracts.DTO;
using ServiceContracts.DTO.ServiceContracts.DTO;

namespace StockMarket.Models
{
    public class Orders
    {
        List<BuyOrderResponse> BuyOrders { get; set; }
        List<SellOrderResponse> SellOrders { get; set; } 
    }
}
