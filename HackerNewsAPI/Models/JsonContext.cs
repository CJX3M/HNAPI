using HackerNewsAPI.Responses;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HackerNewsAPI.Models
{
    [JsonSerializable(typeof(Story))]
    [JsonSerializable(typeof(int[]))]
    [JsonSerializable(typeof(StoryResponse))]
    [JsonSerializable(typeof(List<StoryResponse>))]
    public partial class JsonContext : JsonSerializerContext
    {
    }
}
