using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.DTO.ServiceContracts.DTO;
using Services;

namespace StocksServiceTests
{
    public class StocksServiceTest
    {
        private readonly IStocksService _stocksService;

        public StocksServiceTest()
        {
            _stocksService = new StocksService();
        }

        #region CreateBuyOrder

        [Fact]
        public void CreateBuyOrder_Null()
        {
            BuyOrderRequest? buyOrderRequest = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        [Theory]
        [InlineData(0)]
        public void CreateBuyOrder_TooSmallQuantity(uint buyOrderQuantity)
        {
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", Price = 1, Quantity = buyOrderQuantity };

            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        [Theory] //Use [Theory] instead of [Fact]; so that, you can pass parameters to the test method
        [InlineData(100001)] //passing parameters to the test method
        public void CreateBuyOrder_TooLargeQuantity(uint buyOrderQuantity)
        {
            //Arrange
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", Price = 1, Quantity = buyOrderQuantity };

            //Act
            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        [Theory] //Use [Theory] instead of [Fact]; so that, you can pass parameters to the test method
        [InlineData(0)] //passing parameters to the test method
        public void CreateBuyOrder_TooSmallPrice(uint buyOrderPrice)
        {
            //Arrange
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", Price = buyOrderPrice, Quantity = 1 };

            //Act
            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        [Theory] //Use [Theory] instead of [Fact]; so that, you can pass parameters to the test method
        [InlineData(10001)] //passing parameters to the test method
        public void CreateBuyOrder_TooLargePrice(uint buyOrderPrice)
        {
            //Arrange
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", Price = buyOrderPrice, Quantity = 1 };

            //Act
            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        [Fact]
        public void CreateBuyOrder_StockSymbolIsNull()
        {
            //Arrange
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest() { StockSymbol = null, StockName = "Microsoft", Price = 1, Quantity = 1 };

            //Act
            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        [Fact]
        public void CreateBuyOrder_OrderIsTooOld()
        {
            //Arrange
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", DateAndTimeOfOrder = Convert.ToDateTime("1999-12-31"), Price = 1, Quantity = 1 };

            //Act
            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        [Fact]
        public void CreateBuyOrder_ValidData()
        {
            //Arrange
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", DateAndTimeOfOrder = Convert.ToDateTime("2024-05-27"), Price = 1, Quantity = 1 };

            //Act
            BuyOrderResponse buyOrderResponseFromCreate = _stocksService.CreateBuyOrder(buyOrderRequest);

            //Assert
            Assert.NotEqual(Guid.Empty, buyOrderResponseFromCreate.BuyOrderID);
        }

        #endregion

        #region CreateSellOrder

        [Fact]
        public void CreateSellOrder_Null()
        {
            SellOrderRequest? sellOrderRequest = null;
            Assert.Throws<ArgumentNullException>(() =>
            {
                _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        [Theory]
        [InlineData(0)]
        public void CreateSellOrder_TooSmallQuantity(uint sellOrderQuantity)
        {
            SellOrderRequest? sellOrderRequest = new SellOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", Price = 1, Quantity = sellOrderQuantity };
            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        [Theory]
        [InlineData(100001)]
        public void CreateSellOrder_TooLargeQuantity(uint sellOrderQuantity)
        {
            SellOrderRequest? sellOrderRequest = new SellOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", Price = 1, Quantity = sellOrderQuantity };
            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        [Theory]
        [InlineData(0)]
        public void CreateSellOrder_TooSmallPrice(uint sellOrderPrice)
        {
            SellOrderRequest? sellOrderRequest = new SellOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", Price = sellOrderPrice, Quantity = 1 };
            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        [Theory]
        [InlineData(10001)]
        public void CreateSellOrder_TooLargePrice(uint sellOrderPrice)
        {
            SellOrderRequest? sellOrderRequest = new SellOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", Price = sellOrderPrice, Quantity = 1 };
            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        [Fact]
        public void CreateSellOrder_StockSymbolIsNull()
        {
            SellOrderRequest? sellOrderRequest = new SellOrderRequest() { StockSymbol = null, StockName = "Microsoft", Price = 1, Quantity = 1 };
            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        [Fact]
        public void CreateSellOrder_OrderIsTooOld()
        {
            SellOrderRequest? sellOrderRequest = new SellOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", DateAndTimeOfOrder = Convert.ToDateTime("1999-12-31"), Price = 1, Quantity = 1 };
            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        [Fact]
        public void CreateSellOrder_ValidData()
        {
            SellOrderRequest? sellOrderRequest = new SellOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", DateAndTimeOfOrder = Convert.ToDateTime("2024-05-27"), Price = 1, Quantity = 1 };

            SellOrderResponse sellOrderResponseFromCreate = _stocksService.CreateSellOrder(sellOrderRequest);

            Assert.NotEqual(Guid.Empty, sellOrderResponseFromCreate.SellOrderID);

        }

        #endregion

        #region GetBuyOrders
        [Fact]
        public void GetBuyOrders_Default()
        {
            List<BuyOrderResponse> buyOrdersFromGet = _stocksService.GetBuyOrders();

            Assert.Empty(buyOrdersFromGet);
        }

        [Fact]
        public void GetBuyOrders_NotEmpty()
        {
            BuyOrderRequest buyOrderRequest1 = new BuyOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", DateAndTimeOfOrder = DateTime.Parse("2024-01-01 9:00"), Price = 1, Quantity = 1 };
            BuyOrderRequest buyOrderRequest2 = new BuyOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", DateAndTimeOfOrder = DateTime.Parse("2024-01-01 9:00"), Price = 1, Quantity = 1 };

            List<BuyOrderRequest> buyOrderRequests = new List<BuyOrderRequest> { buyOrderRequest1, buyOrderRequest2 };

            List<BuyOrderResponse> buyOrderResponsesFromAdd = new List<BuyOrderResponse>();

            foreach (BuyOrderRequest buyOrderRequest in buyOrderRequests)
            {
                BuyOrderResponse buyOrderResponse = _stocksService.CreateBuyOrder(buyOrderRequest);
                buyOrderResponsesFromAdd.Add(buyOrderResponse);
            }

            List<BuyOrderResponse> buyOrderResponsesFromGet = _stocksService.GetBuyOrders();

            foreach (BuyOrderResponse buyOrderResponseFromAdd in buyOrderResponsesFromAdd)
            {
                Assert.Contains(buyOrderResponseFromAdd, buyOrderResponsesFromGet);
            }
        }

        #endregion

        #region GetSellOrders

        [Fact]
        public void GetSellOrders_Default()
        {
            List<SellOrderResponse> sellOrderResponses = _stocksService.GetSellOrders();

            Assert.Empty(sellOrderResponses);
        }

        [Fact]
        public void GetSellOrders_NotEmpty()
        {
            SellOrderRequest sellOrderRequest1 = new SellOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", DateAndTimeOfOrder = DateTime.Parse("2024-01-01 9:00"), Price = 1, Quantity = 1 };
            SellOrderRequest sellOrderRequest2 = new SellOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", DateAndTimeOfOrder = DateTime.Parse("2024-01-01 9:00"), Price = 1, Quantity = 1 };

            List<SellOrderRequest> sellOrderRequests = new List<SellOrderRequest>() { sellOrderRequest1, sellOrderRequest2 };

            List<SellOrderResponse> sellOrderResponsesFromAdd = new List<SellOrderResponse>();

            foreach (SellOrderRequest sellOrderRequest in sellOrderRequests)
            {
                SellOrderResponse sellOrderResponse = _stocksService.CreateSellOrder(sellOrderRequest);
                sellOrderResponsesFromAdd.Add(sellOrderResponse);
            }

            List<SellOrderResponse> sellOrdersResponsesFromGet = _stocksService.GetSellOrders();

            foreach (SellOrderResponse sellOrderResponseFromAdd in sellOrderResponsesFromAdd)
            {
                Assert.Contains(sellOrderResponseFromAdd, sellOrdersResponsesFromGet);
            }
        }

        #endregion
    }
}