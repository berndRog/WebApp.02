using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using WebApp.DomainModel.Model;
namespace WebApp.Persistence;

public class DataContextFake : IDataContext {
   
   public Dictionary<Guid, Book> Books { get; } = new();

   public DataContextFake() {
      // File path to read from
      string pathAppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
      var filePath = pathAppData + "/WebApp03.json";
      if (!File.Exists(filePath)) {
         Books = new Dictionary<Guid, Book>();
         return;
      }
      else {
         // Read JSON from file
         string json = File.ReadAllText(filePath, Encoding.UTF8) ??
            throw new ArgumentNullException("File.ReadAllText(filePath, Encoding.UTF8)");
         Books = JsonSerializer.Deserialize<Dictionary<Guid, Book>>(json)
            ?? throw new Exception("JsonSerializer.Deserialize is null)");
      }
   }

   public bool SaveAllChanges() {
      try {
         string json = JsonSerializer.Serialize(
            Books,
            new JsonSerializerOptions { WriteIndented = true }
         );
         // File path to write to
         string pathAppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
         var filePath = pathAppData + "/WebApp03.json";
         // Write JSON string to file
         File.WriteAllText(filePath, json, Encoding.UTF8);
         return true;
      }
      catch (Exception e) {
         Console.WriteLine(e.Message);
         return false;
      }
   }
}