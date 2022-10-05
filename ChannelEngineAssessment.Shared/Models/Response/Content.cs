using System.Text.Json.Serialization;

namespace ChannelEngineAssessment.Shared.Models;

internal class Content
{
    [JsonPropertyName(nameof(Lines))]
    public IEnumerable<Line>? Lines { get; set; }
    [JsonPropertyName(nameof(Name))]
    public string? Name { get; set; }
    [JsonPropertyName(nameof(Ean))]
    public string? Ean { get; set; }
    [JsonPropertyName(nameof(Stock))]
    public int Stock { get; set; }
}
