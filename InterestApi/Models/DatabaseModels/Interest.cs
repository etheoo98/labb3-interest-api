using System.ComponentModel.DataAnnotations;

namespace InterestApi.Models.DatabaseModels;

public class Interest
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(200, MinimumLength = 2)]
    public string Description { get; set; } = null!;

    public ICollection<PersonInterest> PersonInterests { get; set; } = [];
    public ICollection<InterestLink> InterestLinks { get; set; } = []; // TODO: Dependency loop
}