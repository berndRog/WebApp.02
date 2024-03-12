using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Primitives;
using WebApp.Model;
using WebApp.Persistence;
using WebApp.Views;
namespace WebApp.Routing;

public static class Map {

   // S T A T I C   F I L E S
   // -----------------------
   // Static Files Routing
   public static void StaticFilesRoutes(WebApplication app) {
      app.MapGet("/",      () => GetStaticFiles(app, "index.html"));
      // http://localhost:5100/index.html
      app.MapGet("/index", () => GetStaticFiles(app, "index.html"));
      // http://localhost:5010/page1
      // http://localhost:5010/page2
      app.MapGet("/page1", () => GetStaticFiles(app, "page1.html"));
      app.MapGet("/page2", () => GetStaticFiles(app, "page2.html"));
   }

   // Static Files Endpoint Handler
   private static IResult GetStaticFiles(WebApplication app, string page) {
      string filePath = Path.Combine(app.Environment.WebRootPath, page);
      string content = File.ReadAllText(filePath);
      return Results.Content(content, "text/html");
   }

   
   // B O O K S 
   // -----------------------
   private static readonly BooksRepository repository =  new BooksRepository();
   
   // Books Routing
   public static void BookRoutes(WebApplication app) {
      app.MapGet   ("/products/books", Get);
      app.MapGet   ("/products/books/{id:guid}", GetById);
      app.MapPost  ("/products/books", Post); 
      app.MapDelete("/products/books/{id:guid}", Delete);  
   }
   
   // Books endpoint handlers
   private static IResult Get() {
      // find data
      IEnumerable<Book> books = repository.Select().OrderBy(b => b.Id);
      // create HTML view
      var html = HtmlViews.BooksList(books);
      // return HTML view as HTTP response
      return Results.Content(html, "text/html");
   }

   private static IResult GetById(Guid id) {
      // find data
      var book = repository.FindById(id);
      if (book == null) return Results.NotFound("Book not found.");
      // create HTML view
      var html = HtmlViews.BookDetails(book);
      // return HTML view as HTTP response 
      return Results.Content(html, "text/html");
   }

   // Book is posted as form data (table form)
   private static IResult Post(HttpContext context) {

      // Ensure the request content type is x-www-form-urlencoded
      if (!context.Request.HasFormContentType)
         return Results.BadRequest("This request is not a valid form submission.");

      // body a key/value form file
      IFormCollection form = context.Request.Form;
      
      // Validate and parse the Guid
      if (!Guid.TryParse(form["id"], out Guid id))
         return Results.BadRequest("Invalid ID format.");
      // Validate and parse the string
      string author = form["author"].ToString() ?? "";
      string title  = form["title"].ToString() ?? "";
      // Validate and parse the year
      if(!int.TryParse(form["year"], out int year))
         return Results.BadRequest("Invalid year format.");

      // create a book object
      Book book = new Book {
         Id = id,
         Author = author,
         Title = title,
         Year = year
      };
      
      // save data
      if (repository.FindById(book.Id) != null)
         return Results.BadRequest("Book already exists.");
      repository.Add(book);
      // return HTTP response
      return Results.Created($"/books/{book.Id}", book);
   }
   
   private static IResult Delete(Guid id) {
      // find book
      var book = repository.FindById(id);
      if(book == null) return Results.NotFound("Delete: Book not found.");
      // remove book
      repository.Delete(book);
      // return HTTP response
      return Results.NoContent();
      if(book == null) return Results.NotFound("Book not found.");

   }
}

