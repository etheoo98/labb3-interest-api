using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InterestApi.Models.DTOs.RequestDTOs;

public class AddPersonDto
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    [JsonPropertyName("first-name")]
    public string FirstName { get; set; } = null!;

    [Required]
    [StringLength(100, MinimumLength = 2)]
    [JsonPropertyName("last-name")]
    public string LastName { get; set; } = null!;

    [Required]
    [StringLength(10, MinimumLength = 7)]
    [JsonPropertyName("phone-number")]
    public string PhoneNumber { get; set; } = null!;
}