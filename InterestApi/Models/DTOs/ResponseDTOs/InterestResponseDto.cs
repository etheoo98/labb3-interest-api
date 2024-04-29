using System.Text.Json.Serialization;

namespace InterestApi.Models.DTOs.ResponseDTOs;

public class InterestResponseDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("description")]
    public string Description { get; set; } = null!;

    [JsonPropertyName("links")]
    public List<InterestLinkResponseDto> LinkResponseDtos { get; set; } = [];
}