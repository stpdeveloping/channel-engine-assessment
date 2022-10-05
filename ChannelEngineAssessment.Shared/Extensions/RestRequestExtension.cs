using RestSharp;

namespace ChannelEngineAssessment.Shared.Extensions;

static internal class RestRequestExtension
{
    public static RestRequest AddApiKey(this RestRequest request, string apiKey) =>
        request.AddQueryParameter(nameof(apiKey).ToLower(), apiKey);
}
