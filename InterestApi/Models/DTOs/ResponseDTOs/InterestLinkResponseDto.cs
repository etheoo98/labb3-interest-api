using System.Text.Json.Serialization;

namespace InterestApi.Models.DTOs.ResponseDTOs;

public class InterestLinkResponseDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("interest-id")]
    public int InterestId { get; set; }
    
    [JsonPropertyName("interest-name")]
    public string InterestName { get; set; } = null!;

    [JsonPropertyName("link")]
    public string Link { get; set; } = null!;
}