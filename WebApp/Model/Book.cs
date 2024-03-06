using System;
namespace WebApp.Model;

public record Book {
   public Guid Id { get; set; } = Guid.NewGuid();
   public string Author { get; set; } = string.Empty;
   public string Title { get; set; } = string.Empty;
   public int Year { get; set; } = 0;
}