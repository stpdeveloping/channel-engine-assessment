using System.Text.Json.Serialization;

namespace ChannelEngineAssessment.Shared.Models;

internal class Response
{
    [JsonPropertyName(nameof(Content))]
    public IEnumerable<Content>? Contents { get; set; }
}
