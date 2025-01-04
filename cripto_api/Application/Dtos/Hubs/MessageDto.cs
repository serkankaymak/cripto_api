using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Dtos.Hubs;

public class MessageDto : IDto
{
    [JsonPropertyName("content")]
    public string Content { get; private set; }

    [JsonPropertyName("title")]
    public string Title { get; private set; }

    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; } = DateTime.UtcNow;

    [JsonConstructor]
    public MessageDto(string title, string content)
    {
        Title = title;
        Content = content;
    }
}