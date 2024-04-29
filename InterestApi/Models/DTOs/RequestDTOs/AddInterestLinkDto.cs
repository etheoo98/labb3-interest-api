using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InterestApi.Models.DTOs.RequestDTOs;

public class AddInterestLinkDto
{
    [Required]
    [StringLength(400, MinimumLength = 11)]
    [JsonPropertyName("link")]
    public string Link { get; set; } = null!;

    [Required]
    [JsonPropertyName("interest-id")]
    public int InterestId { get; set; }
    
    [Required]
    [JsonPropertyName("person-id")]
    public int PersonId { get; set; }
}