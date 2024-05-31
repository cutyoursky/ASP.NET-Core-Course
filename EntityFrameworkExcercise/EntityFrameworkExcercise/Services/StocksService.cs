using Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;

namespace Services
{
    public class StocksService : IStocksService
    {
        private readonly StockMarketDbContext _db;

        public StocksService(StockMarketDbContext stockMarketDbContext)
        {
            _db = stockMarketDbContext;
        }

        public async Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
        {
            if (buyOrderRequest == null)
            {
                throw new ArgumentNullException(nameof(buyOrderRequest));
            }

            ValidationHelper.ModelValidation(buyOrderRequest);

            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();

            buyOrder.BuyOrderId = Guid.NewGuid();

            _db.BuyOrders.Add(buyOrder);
            await _db.SaveChangesAsync();

            return buyOrder.ToBuyOrderResponse();
        }

        public async Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest)
        {
            if (sellOrderRequest == null)
            {
                throw new ArgumentNullException(nameof(sellOrderRequest));
            }

            ValidationHelper.ModelValidation(sellOrderRequest);

            SellOrder sellOrder = sellOrderRequest.ToSellOrder();

            sellOrder.SellOrderID = Guid.NewGuid();

            _db.SellOrders.Add(sellOrder);
            await _db.SaveChangesAsync();

            return sellOrder.ToSellOrderResponse();
        }

        public async  Task<List<BuyOrderResponse>> GetBuyOrders()
        {
            List<BuyOrder> buyOrders = await _db.BuyOrders
                .OrderByDescending(temp => temp.DateAndTimeOfOrder)
                .ToListAsync();

            return buyOrders.Select(temp => temp.ToBuyOrderResponse()).ToList();
        }

        public async Task<List<SellOrderResponse>> GetSellOrders()
        {
            List<SellOrder> sellOrders = await _db.SellOrders
                .OrderByDescending(temp => temp.DateAndTimeOfOrder)
                .ToListAsync();

            return sellOrders.Select(temp => temp.ToSellOrderResponse()).ToList();
        }
    }
}
