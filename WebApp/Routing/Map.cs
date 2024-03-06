using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using WebApp.Views;
using WebApp.Model;
using WebApp.Persistence;
namespace WebApp;

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

   private static IResult Post(Book book) {
      // save data
      if (repository.FindById(book.Id) != null)
         return Results.BadRequest("Book already exists.");
      repository.Add(book);
      // return HTTP response
      return Results.Created($"/books/{book.Id}", book);
   }

   private static IResult Delete(Guid id) {
      // remove data
      var book = repository.FindById(id);
      repository.Delete(book);
      // return HTTP response
      return Results.NoContent();
   }
}

