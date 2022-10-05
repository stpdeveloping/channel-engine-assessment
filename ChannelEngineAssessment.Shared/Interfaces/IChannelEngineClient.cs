using RestSharp;
using static ChannelEngineAssessment.Shared.ChannelEngineClient;

namespace ChannelEngineAssessment.Shared.Interfaces;

public interface IChannelEngineClient
{
    IAsyncEnumerable<SoldProduct> GetTopFiveBestSellingProductsAsync();
    ValueTask<RestResponse> PatchProductStockAsync(string merchantProductNo, int stock);
}
