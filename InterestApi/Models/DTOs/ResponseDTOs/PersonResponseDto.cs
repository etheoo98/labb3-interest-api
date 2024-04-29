using System.Text.Json.Serialization;
using InterestApi.Models.DatabaseModels;

namespace InterestApi.Models.DTOs.ResponseDTOs;

public class PersonResponseDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("first-name")]
    public string FirstName { get; set; } = null!;

    [JsonPropertyName("last-name")]
    public string LastName { get; set; } = null!;

    [JsonPropertyName("phone-number")]
    public string PhoneNumber { get; set; } = null!;

    [JsonPropertyName("interests")]
    public List<InterestResponseDto> InterestDtos { get; set; } = [];
}