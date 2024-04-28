using System.Text.Json.Serialization;
namespace api_net8.Domain.Models.Enum

{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Role
    {
        admin = 1,
        author = 2,
        editor = 3,
        subscriber = 4,
    }
}