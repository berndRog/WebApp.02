using System;
using System.ComponentModel.DataAnnotations;
namespace WebApp.Model;

// public record Book {
//    public Guid Id { get; set; } = Guid.NewGuid();
//    public string Author { get; set; } = string.Empty;
//    public string Title { get; set; } = string.Empty;
//    public int Year { get; set; } = 0;
// }


// With Validation
public record Book  {
   [Required]
   public Guid Id { get; set; }

   [Required]
   [StringLength(100, MinimumLength = 3)]
   public string Author { get; set; }

   [Required]
   [StringLength(100, MinimumLength = 3)]
   public string Title { get; set; }

   [Required]
   [Range(1900, 2099)]
   public int Year { get; set; }
}