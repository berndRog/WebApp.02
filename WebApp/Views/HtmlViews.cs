using System.Collections.Generic;
using WebApp.Model;
namespace WebApp.Views;
// Just as a basic example.
// !!! Please DO NOT USE like this!!!
//
// Blazor / Razor: HTML code is used here and with
// the help of C# helpers tags "@" C# code is embedded
// in HTML
// (see lecture Web Clients)   

public static class HtmlViews {
   // Fake HTML views with string interpolation
   public static string BookDetails(Book book) {
      string html =
         "<!DOCTYPE html>\n" +
         "<html lang=\"de\">\n" +
         " <head>\n" +
         " <meta charset=\"UTF-8\"\n>" +
         " <title>Buchdetails</title>\n" +
         "<head>\n" +
         "<body>\n" +
         " <h2>Buchdetails</h2>\n" +
         $"<p>ID: {book.Id.ToString()[0..10]}</p>" +
         $"<p>Autor: {book.Author}</p>" +
         $"<p>Titel: {book.Title}</p>" +
         $"<p>Jahr: {book.Year}</p>" +
         "</body>" +
         "</html>";
      return html;
   }

   public static string BooksList(IEnumerable<Book> books) {
      string html =
         "<!DOCTYPE html>\n" +
         "<html lang=\"de\">\n" +
         "<head>\n" +
         "<meta charset=\"UTF-8\">\n" +
         "<title>Bücherliste</title>\n" +
         "<head>\n" +
         "<body>\n" +
         "<h2>Bücherliste</h2>\n" +
         "<table border=\"0\">\n" +
         "<thead>\n" +
         "<tr>\n" +
         "<th>ID</th>\n" +
         "<th>Autor</th>\n" +
         "<th>Titel</th>\n" +
         "<th>Jahr</th>\n" +
         "</tr>\n" +
         "</thead>\n" +
         "<tbody>\n";

      foreach (var book in books) {
         html +=
            "<tr>\n" +
            $"<td>{book.Id.ToString()[0..10]}</td>\n" +
            $"<td>{book.Author}</td>\n" +
            $"<td>{book.Title}</td>\n" +
            $"<td>{book.Year}</td>\n" +
            "</tr>\n";
      }
      html +=
         "</tbody>\n" +
         "</table>\n" +
         "</body>\n" +
         "</html>";

      return html;
   }
}