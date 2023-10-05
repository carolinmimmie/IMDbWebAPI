
using System.ComponentModel.DataAnnotations;

namespace IMDbWebAPI.Domain;

public class Movie
{
  public int Id { get; set; }
  
  [Required]
  [MaxLength(50)]
  public string Title { get; set; }

  [Required]
  [MaxLength(500)]
  public string Plot { get; set; }

  [Required]
  [MaxLength(50)]
  public string Genre { get; set; }

  [Required]
  [MaxLength(50)]
  public string Director { get; set; }

  public int ReleaseYear { get; set; }
}