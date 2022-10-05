using System.Text.Json.Serialization;

namespace ChannelEngineAssessment.Shared.Models;

internal class PatchProductStockRequest
{
    [JsonPropertyName("op")]
    public string? Operation { get; set; }
    [JsonPropertyName("value")]
    public int Value { get; set; }
    [JsonPropertyName("path")]
    public string? Property { get; set; }
}
