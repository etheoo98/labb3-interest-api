using System.Text.Json.Serialization;

namespace InterestApi.Models.DTOs.RequestDTOs;

public class AddPersonInterestDto
{
    [JsonPropertyName("person-id")]
    public int PersonId { get; set; }
    
    [JsonPropertyName("interest-id")]
    public int InterestId { get; set; }
}