using ChannelEngineAssessment.Shared.Extensions;
using ChannelEngineAssessment.Shared.Interfaces;
using ChannelEngineAssessment.Shared.Models;
using RestSharp;

namespace ChannelEngineAssessment.Shared;

public class ChannelEngineClient : IChannelEngineClient
{
	public record SoldProduct(string MerchantProductNo, string Name, string Ean, 
		int TotalQuantity, int Stock);

	private readonly RestClient restClient;
	private readonly string apiKey;

	public ChannelEngineClient(RestClient restClient, string apiKey)
	{
		this.restClient = restClient;
		this.apiKey = apiKey;
	}

	public async IAsyncEnumerable<SoldProduct> GetTopFiveBestSellingProductsAsync()
	{
		IEnumerable<Line>? productsFromOrders = await GetProductsFromOrdersAsync();
		var productMetricsOrderedByTotalQuantity = productsFromOrders?.GroupBy(line => 
			line.MerchantProductNo).Select(productCollection => new
			{
				ProductId = productCollection.Key,
				TotalQuantity = productCollection.Sum(line => line.Quantity)
			}).Take(5).OrderByDescending(productMetric => productMetric.TotalQuantity);
		foreach (var productMetric in productMetricsOrderedByTotalQuantity!)
			yield return await GetSoldProductAsync(productMetric.ProductId!,
				productMetric.TotalQuantity);
	}

	public async ValueTask<RestResponse> PatchProductStockAsync(string merchantProductNo, 
		int stock) => await restClient.PatchAsync(new RestRequest(
				$"products/{merchantProductNo}").AddApiKey(apiKey).AddJsonBody(
					new PatchProductStockRequest[] { new() 
					{
						Operation = "replace",
						Value = stock,
						Property = "Stock"
					}}));

	private async ValueTask<IEnumerable<Line>?> GetProductsFromOrdersAsync()
	{
        var response = await restClient.GetAsync<Response>(new RestRequest("orders")
			.AddQueryParameter("statuses", "IN_PROGRESS").AddApiKey(apiKey));
		return response?.Contents?.SelectMany(content => content.Lines!);
    }

	private async ValueTask<SoldProduct> GetSoldProductAsync(string merchantProductNo, 
		int totalQuantity)
	{
		var response = await restClient.GetAsync<Response>(new RestRequest("products")
			.AddQueryParameter("search", merchantProductNo).AddApiKey(apiKey));
		Content responseContent = response!.Contents?.First() ?? 
			throw new NullReferenceException(nameof(responseContent));
		return new SoldProduct(merchantProductNo, responseContent.Name!, 
			responseContent.Ean!, totalQuantity, responseContent.Stock);
	}
}