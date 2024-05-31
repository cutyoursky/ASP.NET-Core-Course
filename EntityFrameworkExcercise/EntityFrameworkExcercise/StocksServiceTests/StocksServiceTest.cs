using Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;

namespace StocksServiceTests
{
    public class StocksServiceTest
    {
        private readonly IStocksService _stocksService;

        public StocksServiceTest()
        {
            _stocksService = new StocksService(new StockMarketDbContext(new DbContextOptions<StockMarketDbContext>()));
        }

        #region CreateBuyOrder

        [Fact]
        public async Task CreateBuyOrder_Null()
        {
            BuyOrderRequest? buyOrderRequest = null;

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        [Theory]
        [InlineData(0)]
        public async Task CreateBuyOrder_TooSmallQuantity(uint buyOrderQuantity)
        {
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", Price = 1, Quantity = buyOrderQuantity };

            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        [Theory] //Use [Theory] instead of [Fact]; so that, you can pass parameters to the test method
        [InlineData(100001)] //passing parameters to the test method
        public async Task CreateBuyOrder_TooLargeQuantity(uint buyOrderQuantity)
        {
            //Arrange
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", Price = 1, Quantity = buyOrderQuantity };

            //Act
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        [Theory] //Use [Theory] instead of [Fact]; so that, you can pass parameters to the test method
        [InlineData(0)] //passing parameters to the test method
        public async Task CreateBuyOrder_TooSmallPrice(uint buyOrderPrice)
        {
            //Arrange
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", Price = buyOrderPrice, Quantity = 1 };

            //Act
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        [Theory] //Use [Theory] instead of [Fact]; so that, you can pass parameters to the test method
        [InlineData(10001)] //passing parameters to the test method
        public async Task CreateBuyOrder_TooLargePrice(uint buyOrderPrice)
        {
            //Arrange
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", Price = buyOrderPrice, Quantity = 1 };

            //Act
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        [Fact]
        public async Task CreateBuyOrder_StockSymbolIsNull()
        {
            //Arrange
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest() { StockSymbol = null, StockName = "Microsoft", Price = 1, Quantity = 1 };

            //Act
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        [Fact]
        public async Task CreateBuyOrder_OrderIsTooOld()
        {
            //Arrange
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", DateAndTimeOfOrder = Convert.ToDateTime("1999-12-31"), Price = 1, Quantity = 1 };

            //Act
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        [Fact]
        public async Task CreateBuyOrder_ValidData()
        {
            //Arrange
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", DateAndTimeOfOrder = Convert.ToDateTime("2024-05-27"), Price = 1, Quantity = 1 };

            //Act
            BuyOrderResponse buyOrderResponseFromCreate = await _stocksService.CreateBuyOrder(buyOrderRequest);

            //Assert
            Assert.NotEqual(Guid.Empty, buyOrderResponseFromCreate.BuyOrderID);
        }
        
        #endregion

        #region CreateSellOrder

        [Fact]
        public async Task CreateSellOrder_Null()
        {
            SellOrderRequest? sellOrderRequest = null;
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        [Theory]
        [InlineData(0)]
        public async Task CreateSellOrder_TooSmallQuantity(uint sellOrderQuantity)
        {
            SellOrderRequest? sellOrderRequest = new SellOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", Price = 1, Quantity = sellOrderQuantity };
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        [Theory]
        [InlineData(100001)]
        public async Task CreateSellOrder_TooLargeQuantity(uint sellOrderQuantity)
        {
            SellOrderRequest? sellOrderRequest = new SellOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", Price = 1, Quantity = sellOrderQuantity };
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        [Theory]
        [InlineData(0)]
        public async Task CreateSellOrder_TooSmallPrice(uint sellOrderPrice)
        {
            SellOrderRequest? sellOrderRequest = new SellOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", Price = sellOrderPrice, Quantity = 1 };
            await Assert.ThrowsAsync<ArgumentException>(async() =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        [Theory]
        [InlineData(10001)]
        public async Task CreateSellOrder_TooLargePrice(uint sellOrderPrice)
        {
            SellOrderRequest? sellOrderRequest = new SellOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", Price = sellOrderPrice, Quantity = 1 };
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        [Fact]
        public async Task CreateSellOrder_StockSymbolIsNull()
        {
            SellOrderRequest? sellOrderRequest = new SellOrderRequest() { StockSymbol = null, StockName = "Microsoft", Price = 1, Quantity = 1 };
            await Assert.ThrowsAsync<ArgumentException>(async() =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        [Fact]
        public async Task CreateSellOrder_OrderIsTooOld()
        {
            SellOrderRequest? sellOrderRequest = new SellOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", DateAndTimeOfOrder = Convert.ToDateTime("1999-12-31"), Price = 1, Quantity = 1 };
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        [Fact]
        public async Task CreateSellOrder_ValidData()
        {
            SellOrderRequest? sellOrderRequest = new SellOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", DateAndTimeOfOrder = Convert.ToDateTime("2024-05-27"), Price = 1, Quantity = 1 };

            SellOrderResponse sellOrderResponseFromCreate = await _stocksService.CreateSellOrder(sellOrderRequest);

            Assert.NotEqual(Guid.Empty, sellOrderResponseFromCreate.SellOrderID);

        }

        #endregion

        #region GetBuyOrders
        [Fact]
        public async Task GetBuyOrders_Default()
        {
            List<BuyOrderResponse> buyOrdersFromGet = await _stocksService.GetBuyOrders();

            Assert.Empty(buyOrdersFromGet);
        }

        [Fact]
        public async Task GetBuyOrders_NotEmpty()
        {
            BuyOrderRequest buyOrderRequest1 = new BuyOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", DateAndTimeOfOrder = DateTime.Parse("2024-01-01 9:00"), Price = 1, Quantity = 1 };
            BuyOrderRequest buyOrderRequest2 = new BuyOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", DateAndTimeOfOrder = DateTime.Parse("2024-01-01 9:00"), Price = 1, Quantity = 1 };

            List<BuyOrderRequest> buyOrderRequests = new List<BuyOrderRequest> { buyOrderRequest1, buyOrderRequest2 };

            List<BuyOrderResponse> buyOrderResponsesFromAdd = new List<BuyOrderResponse>();

            foreach (BuyOrderRequest buyOrderRequest in buyOrderRequests)
            {
                BuyOrderResponse buyOrderResponse = await _stocksService.CreateBuyOrder(buyOrderRequest);
                buyOrderResponsesFromAdd.Add(buyOrderResponse);
            }

            List<BuyOrderResponse> buyOrderResponsesFromGet = await _stocksService.GetBuyOrders();

            foreach (BuyOrderResponse buyOrderResponseFromAdd in buyOrderResponsesFromAdd)
            {
                Assert.Contains(buyOrderResponseFromAdd, buyOrderResponsesFromGet);
            }
        }

        #endregion

        #region GetSellOrders

        [Fact]
        public async Task GetSellOrders_Default()
        {
            List<SellOrderResponse> sellOrderResponses = await _stocksService.GetSellOrders();

            Assert.Empty(sellOrderResponses);
        }

        [Fact]
        public async Task GetSellOrders_NotEmpty()
        {
            SellOrderRequest sellOrderRequest1 = new SellOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", DateAndTimeOfOrder = DateTime.Parse("2024-01-01 9:00"), Price = 1, Quantity = 1 };
            SellOrderRequest sellOrderRequest2 = new SellOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", DateAndTimeOfOrder = DateTime.Parse("2024-01-01 9:00"), Price = 1, Quantity = 1 };

            List<SellOrderRequest> sellOrderRequests = new List<SellOrderRequest>() { sellOrderRequest1, sellOrderRequest2 };

            List<SellOrderResponse> sellOrderResponsesFromAdd = new List<SellOrderResponse>();

            foreach (SellOrderRequest sellOrderRequest in sellOrderRequests)
            {
                SellOrderResponse sellOrderResponse = await _stocksService.CreateSellOrder(sellOrderRequest);
                sellOrderResponsesFromAdd.Add(sellOrderResponse);
            }

            List<SellOrderResponse> sellOrdersResponsesFromGet = await _stocksService.GetSellOrders();

            foreach (SellOrderResponse sellOrderResponseFromAdd in sellOrderResponsesFromAdd)
            {
                Assert.Contains(sellOrderResponseFromAdd, sellOrdersResponsesFromGet);
            }
        }

        #endregion
    }
}