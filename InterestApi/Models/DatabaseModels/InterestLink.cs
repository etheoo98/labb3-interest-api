using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterestApi.Models.DatabaseModels;

public class InterestLink
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(400, MinimumLength = 11)]
    public string Link { get; set; } = null!;

    [Required]
    [ForeignKey("Interest")]
    public int InterestId { get; set; } // ID of the interest associated with the link object
    
    [ForeignKey("Person")]
    public int PersonId { get; set; } // ID of the person who added the link object
    
    // Navigational Properties
    public Interest Interest { get; set; } = null!;

    public Person Person { get; set; } = null!;
}