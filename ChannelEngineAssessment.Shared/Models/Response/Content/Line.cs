using System.Text.Json.Serialization;

namespace ChannelEngineAssessment.Shared.Models;

internal class Line
{
    [JsonPropertyName(nameof(MerchantProductNo))]
    public string? MerchantProductNo { get; set; }
    [JsonPropertyName(nameof(Quantity))]
    public int Quantity { get; set; }
}
