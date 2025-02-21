using System.Text.Json.Serialization;

namespace TaskManagerAPI.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Status
{
    Pending = 1,
    InProgress = 2,
    Done = 3
}
