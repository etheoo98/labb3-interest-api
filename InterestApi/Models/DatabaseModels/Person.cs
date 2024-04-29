using System.ComponentModel.DataAnnotations;

namespace InterestApi.Models.DatabaseModels;

public class Person
{
    [Required]
    public int Id { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string FirstName { get; set; } = null!;

    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string LastName { get; set; } = null!;

    [Required]
    [StringLength(10, MinimumLength = 7)]
    public string PhoneNumber { get; set; } = null!;

    public ICollection<PersonInterest> PersonInterests { get; set; } = new List<PersonInterest>();
}