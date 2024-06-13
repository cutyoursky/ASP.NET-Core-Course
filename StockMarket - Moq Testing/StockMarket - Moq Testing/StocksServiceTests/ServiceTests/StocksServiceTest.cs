using AutoFixture;
using Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;

namespace StocksServiceTests.ServiceTests
{
    public class StocksServiceTest
    {
        private readonly IStocksService _stocksService;

        private readonly Mock<IStocksRepository> _stocksRepositoryMock;
        private readonly IStocksRepository _stocksRepository;
        private readonly IFixture _fixture; 

        public StocksServiceTest()
        {
            _fixture = new Fixture();
            _stocksRepositoryMock = new Mock<IStocksRepository>();
            _stocksRepository = _stocksRepositoryMock.Object;
            _stocksService = new StocksService(_stocksRepository);
        }

        #region CreateBuyOrder

        [Fact]
        public async Task CreateBuyOrder_Null()
        {
            BuyOrderRequest? buyOrderRequest = null;

            BuyOrder buyOrderFixture = _fixture.Build<BuyOrder>()
                .Create();
            _stocksRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>())).ReturnsAsync(buyOrderFixture);

            Func<Task> action = async () =>
            {
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            };

            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Theory]
        [InlineData(0)]
        public async Task CreateBuyOrder_TooSmallQuantity(uint buyOrderQuantity)
        {
            BuyOrderRequest? buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                .With(temp => temp.Quantity, buyOrderQuantity)
                .Create();

            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();
            _stocksRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>())).ReturnsAsync(buyOrder);

            Func<Task> action = async () =>
            {
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            };

            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Theory] //Use [Theory] instead of [Fact]; so that, you can pass parameters to the test method
        [InlineData(100001)] //passing parameters to the test method
        public async Task CreateBuyOrder_TooLargeQuantity(uint buyOrderQuantity)
        {
            BuyOrderRequest? buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                .With(temp => temp.Quantity, buyOrderQuantity)
                .Create();

            BuyOrder buyorder = buyOrderRequest.ToBuyOrder();
            _stocksRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>())).ReturnsAsync(buyorder);

            Func<Task> action = async () =>
            {
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            };

            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Theory] //Use [Theory] instead of [Fact]; so that, you can pass parameters to the test method
        [InlineData(0)] //passing parameters to the test method
        public async Task CreateBuyOrder_TooSmallPrice(uint buyOrderPrice)
        {
            BuyOrderRequest? buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                .With(temp => temp.Price, buyOrderPrice)
                .Create();

            BuyOrder buyorder = buyOrderRequest.ToBuyOrder();
            _stocksRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>())).ReturnsAsync(buyorder);

            Func<Task> action = async () =>
            {
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            };

            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Theory] //Use [Theory] instead of [Fact]; so that, you can pass parameters to the test method
        [InlineData(10001)] //passing parameters to the test method
        public async Task CreateBuyOrder_TooLargePrice(uint buyOrderPrice)
        {
            BuyOrderRequest? buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                .With(temp => temp.Price, buyOrderPrice)
                .Create();

            BuyOrder buyorder = buyOrderRequest.ToBuyOrder();
            _stocksRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>())).ReturnsAsync(buyorder);

            Func<Task> action = async () =>
            {
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            };

            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateBuyOrder_StockSymbolIsNull()
        {
            //Arrange
            BuyOrderRequest? buyOrderRequest = _fixture.Build<BuyOrderRequest>()
             .With(temp => temp.StockSymbol, null as string)
             .Create();

            //Mock
            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();
            _stocksRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>())).ReturnsAsync(buyOrder);

            //Act
            Func<Task> action = async () =>
            {
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateBuyOrder_OrderIsTooOld()
        {
            BuyOrderRequest? buyOrderRequest = _fixture.Build<BuyOrderRequest>()
             .With(temp => temp.DateAndTimeOfOrder, Convert.ToDateTime("1999-12-31"))
             .Create();

            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();
            _stocksRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>())).ReturnsAsync(buyOrder);

            Func<Task> action = async () =>
            {
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            };

            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateBuyOrder_ValidData()
        {
            //Arrange
            BuyOrderRequest? buyOrderRequest = _fixture.Build<BuyOrderRequest>()
             .Create();

            //Mock
            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();
            _stocksRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>())).ReturnsAsync(buyOrder);

            //Act
            BuyOrderResponse buyOrderResponseFromCreate = await _stocksService.CreateBuyOrder(buyOrderRequest);

            //Assert
            buyOrder.BuyOrderId = buyOrderResponseFromCreate.BuyOrderID;
            BuyOrderResponse buyOrderResponse_expected = buyOrder.ToBuyOrderResponse();
            buyOrderResponseFromCreate.BuyOrderID.Should().NotBe(Guid.Empty);
            buyOrderResponseFromCreate.Should().Be(buyOrderResponse_expected);
        }

        #endregion

        #region CreateSellOrder

        [Fact]
        public async Task CreateSellOrder_Null()
        {
            //Arrange
            SellOrderRequest? sellOrderRequest = null;

            //Mock
            SellOrder sellOrderFixture = _fixture.Build<SellOrder>()
             .Create();
            _stocksRepositoryMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>())).ReturnsAsync(sellOrderFixture);

            //Act
            Func<Task> action = async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Theory]
        [InlineData(0)]
        public async Task CreateSellOrder_TooSmallQuantity(uint sellOrderQuantity)
        {
            //Arrange
            SellOrderRequest? sellOrderRequest = _fixture.Build<SellOrderRequest>()
             .With(temp => temp.Quantity, sellOrderQuantity)
             .Create();

            //Mock
            SellOrder sellOrder = sellOrderRequest.ToSellOrder();
            _stocksRepositoryMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>())).ReturnsAsync(sellOrder);

            //Act
            Func<Task> action = async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Theory]
        [InlineData(100001)]
        public async Task CreateSellOrder_TooLargeQuantity(uint sellOrderQuantity)
        {
            //Arrange
            SellOrderRequest? sellOrderRequest = _fixture.Build<SellOrderRequest>()
             .With(temp => temp.Quantity, sellOrderQuantity)
             .Create();

            //Mock
            SellOrder sellOrder = sellOrderRequest.ToSellOrder();
            _stocksRepositoryMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>())).ReturnsAsync(sellOrder);

            //Act
            Func<Task> action = async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Theory]
        [InlineData(0)]
        public async Task CreateSellOrder_TooSmallPrice(uint sellOrderPrice)
        {
            //Arrange
            SellOrderRequest? sellOrderRequest = _fixture.Build<SellOrderRequest>()
             .With(temp => temp.Price, sellOrderPrice)
             .Create();

            //Mock
            SellOrder sellOrder = sellOrderRequest.ToSellOrder();
            _stocksRepositoryMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>())).ReturnsAsync(sellOrder);

            //Act
            Func<Task> action = async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Theory]
        [InlineData(10001)]
        public async Task CreateSellOrder_TooLargePrice(uint sellOrderPrice)
        {
            //Arrange
            SellOrderRequest? sellOrderRequest = _fixture.Build<SellOrderRequest>()
             .With(temp => temp.Price, sellOrderPrice)
             .Create();

            //Mock
            SellOrder sellOrder = sellOrderRequest.ToSellOrder();
            _stocksRepositoryMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>())).ReturnsAsync(sellOrder);

            //Act
            Func<Task> action = async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateSellOrder_StockSymbolIsNull()
        {
            //Arrange
            SellOrderRequest? sellOrderRequest = _fixture.Build<SellOrderRequest>()
             .With(temp => temp.StockSymbol, null as string)
             .Create();

            //Mock
            SellOrder sellOrder = sellOrderRequest.ToSellOrder();
            _stocksRepositoryMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>())).ReturnsAsync(sellOrder);

            //Act
            Func<Task> action = async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateSellOrder_OrderIsTooOld()
        {
            //Arrange
            SellOrderRequest? sellOrderRequest = _fixture.Build<SellOrderRequest>()
             .With(temp => temp.DateAndTimeOfOrder, Convert.ToDateTime("1999-12-31"))
             .Create();

            //Mock
            SellOrder sellOrder = sellOrderRequest.ToSellOrder();
            _stocksRepositoryMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>())).ReturnsAsync(sellOrder);

            //Act
            Func<Task> action = async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateSellOrder_ValidData()
        {
            //Arrange
            SellOrderRequest? sellOrderRequest = _fixture.Build<SellOrderRequest>()
             .Create();


            //Mock
            SellOrder sellOrder = sellOrderRequest.ToSellOrder();
            _stocksRepositoryMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>())).ReturnsAsync(sellOrder);

            //Act
            SellOrderResponse sellOrderResponseFromCreate = await _stocksService.CreateSellOrder(sellOrderRequest);

            //Assert
            sellOrder.SellOrderID = sellOrderResponseFromCreate.SellOrderID;
            SellOrderResponse sellOrderResponse_expected = sellOrder.ToSellOrderResponse();
            sellOrderResponseFromCreate.SellOrderID.Should().NotBe(Guid.Empty);
            sellOrderResponseFromCreate.Should().Be(sellOrderResponse_expected);
        }

        #endregion

        #region GetBuyOrders
        [Fact]
        public async Task GetBuyOrders_Default()
        {
            //Arrange
            List<BuyOrder> buyOrders = new List<BuyOrder>();

            //Mock
            _stocksRepositoryMock.Setup(temp => temp.GetBuyOrders()).ReturnsAsync(buyOrders);

            //Act
            List<BuyOrderResponse> buyOrdersFromGet = await _stocksService.GetBuyOrders();

            //Assert
            Assert.Empty(buyOrdersFromGet);
        }

        [Fact]
        public async Task GetBuyOrders_NotEmpty()
        {
            //Arrange
            List<BuyOrder> buyOrder_requests = new List<BuyOrder>() 
            {
                _fixture.Build<BuyOrder>().Create(),
                _fixture.Build<BuyOrder>().Create()
            };

            List<BuyOrderResponse> buyOrders_list_expected = buyOrder_requests.Select(temp => temp.ToBuyOrderResponse()).ToList();
            List<BuyOrderResponse> buyOrder_response_list_from_add = new List<BuyOrderResponse>();

            //Mock
            _stocksRepositoryMock.Setup(temp => temp.GetBuyOrders()).ReturnsAsync(buyOrder_requests);

            //Act
            List<BuyOrderResponse> buyOrders_list_from_get = await _stocksService.GetBuyOrders();


            //Assert
            buyOrders_list_from_get.Should().BeEquivalentTo(buyOrders_list_expected);
        }

        #endregion

        #region GetSellOrders

        [Fact]
        public async Task GetSellOrders_Default()
        {
            //Arrange
            List<SellOrder> sellOrders = new List<SellOrder>();

            //Mock
            _stocksRepositoryMock.Setup(temp => temp.GetSellOrders()).ReturnsAsync(sellOrders);

            //Act
            List<SellOrderResponse> sellOrdersFromGet = await _stocksService.GetSellOrders();

            //Assert
            Assert.Empty(sellOrdersFromGet);
        }

        [Fact]
        public async Task GetSellOrders_NotEmpty()
        {
            //Arrange
            List<SellOrder> sellOrder_requests = new List<SellOrder>() 
            {
                _fixture.Build<SellOrder>().Create(),
                _fixture.Build<SellOrder>().Create()
            };

            List<SellOrderResponse> sellOrders_list_expected = sellOrder_requests.Select(temp => temp.ToSellOrderResponse()).ToList();
            List<SellOrderResponse> sellOrder_response_list_from_add = new List<SellOrderResponse>();

            //Mock
            _stocksRepositoryMock.Setup(temp => temp.GetSellOrders()).ReturnsAsync(sellOrder_requests);

            //Act
            List<SellOrderResponse> sellOrders_list_from_get = await _stocksService.GetSellOrders();


            //Assert
            sellOrders_list_from_get.Should().BeEquivalentTo(sellOrders_list_expected);
        }

        #endregion
    }
}