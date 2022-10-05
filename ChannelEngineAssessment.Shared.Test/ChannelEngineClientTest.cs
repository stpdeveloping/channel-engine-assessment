using ChannelEngineAssessment.Shared.Interfaces;
using RestSharp;

namespace ChannelEngineAssessment.Shared.Test;

[TestClass]
public class ChannelEngineClientTest
{
    private readonly IChannelEngineClient channelEngineClient;

    public ChannelEngineClientTest() =>
        channelEngineClient = new ChannelEngineClient(new RestClient(
                RestClientParams.API_ROUTE), RestClientParams.API_KEY);

    [TestMethod]
    public async Task GetTopFiveBestSellingProductsAsync_ReturnsOrderedProducts()
    {
        await channelEngineClient.GetTopFiveBestSellingProductsAsync().AggregateAsync(
            (previousSoldProduct, nextSoldProduct) =>
            {
                if (previousSoldProduct.TotalQuantity < nextSoldProduct.TotalQuantity)
                    Assert.Fail("Sold products are not ordered properly");
                return nextSoldProduct;
            });
    }
}