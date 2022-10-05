using ChannelEngineAssessment.Shared;
using ChannelEngineAssessment.Shared.Interfaces;
using RestSharp;
using static ChannelEngineAssessment.Shared.ChannelEngineClient;

Console.WriteLine("Top five best selling products:");
IChannelEngineClient channelEngineClient = new ChannelEngineClient(new RestClient(
    RestClientParams.API_ROUTE), RestClientParams.API_KEY);
await foreach (SoldProduct soldProduct in
    channelEngineClient.GetTopFiveBestSellingProductsAsync())
{
    Console.WriteLine($"{soldProduct.Name}, {soldProduct.Ean}, " +
        $"{soldProduct.TotalQuantity}, STOCK: {soldProduct.Stock}");
    Console.WriteLine("Updating stock...");
    await channelEngineClient.PatchProductStockAsync(soldProduct.MerchantProductNo,
        Random.Shared.Next(20));
    Console.WriteLine("Updating stock finished");
}
Console.ReadLine();