using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using WebApp.Model;
namespace WebApp.Persistence;

public class BooksRepository {
   
   private readonly Dictionary<Guid, Book> _books = new();

   public BooksRepository() {
      // File path to read from
      string pathAppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
      var filePath = pathAppData + "/WebApp02.json";
      if(!File.Exists(filePath)) {
         _books = new Dictionary<Guid, Book>();
         return;
      } 
      else {
         // Read JSON from file
         string json = File.ReadAllText(filePath, Encoding.UTF8);
         _books = JsonSerializer.Deserialize<Dictionary<Guid, Book>>(json);   
      }
   }  
   
   public IEnumerable<Book> Select() {
      return _books.Values.ToList();
   }

   public Book? FindById(Guid id) { 
      _books.TryGetValue(id, out Book book);
      return book;
   }

   public void Add(Book book) {
      _books.Add(book.Id, book);
      SaveAllChanges();
   }
   
   public void Delete(Book book) {
      _books.Remove(book.Id);
      SaveAllChanges();
   }
   
   private void SaveAllChanges() {
      string json = JsonSerializer.Serialize(
         _books, 
         new JsonSerializerOptions { WriteIndented = true }
      );
      // File path to write to
      string pathAppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
      var filePath = pathAppData + "/WebApp02.json";
      // Write JSON string to file
      File.WriteAllText(filePath, json, Encoding.UTF8);
   }
}