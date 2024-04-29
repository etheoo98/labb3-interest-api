using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InterestApi.Models.DTOs.RequestDTOs;

public class AddInterestDto
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(200, MinimumLength = 2)]
    [JsonPropertyName("description")]
    public string Description { get; set; } = null!;
}