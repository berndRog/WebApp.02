using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using WebApp.Model;
namespace WebApp.Persistence;

public class DataContextFake {
   
   // File path  WIN: .AppData or Mac ./Library/Application Support
   private readonly string _filePath = 
      Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
      + "/WebApp03.json";
   
   // In-Memory Repository
   public Dictionary<Guid, Book> Books { get; } = new();
   
   public DataContextFake() {
      if (!File.Exists(_filePath)) {
         Books = new Dictionary<Guid, Book>();
         return;
      }
      else {
         // Read JSON from file
         string json = File.ReadAllText(_filePath, Encoding.UTF8) ??
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
         // Write JSON string to file
         File.WriteAllText(_filePath, json, Encoding.UTF8);
         return true;
      }
      catch (Exception e) {
         Console.WriteLine(e.Message);
         return false;
      }
   }
}