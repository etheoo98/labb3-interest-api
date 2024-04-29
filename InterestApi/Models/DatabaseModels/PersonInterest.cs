using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterestApi.Models.DatabaseModels;

public class PersonInterest
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [ForeignKey("Person")]
    public int PersonId { get; set; }
    public Person Person { get; set; } = null!;

    [Required]
    [ForeignKey("Interest")]
    public int InterestId { get; set; }
    public Interest Interest { get; set; } = null!;
}