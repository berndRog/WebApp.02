using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using WebApp.Model;

namespace WebApp.Persistence;

public class BooksRepositoryFake(
   DataContextFake dataContext
) {

   public IEnumerable<Book> Select() {
      return dataContext.Books.Values.ToList();
   }

   public Book? FindById(Guid id) { 
      dataContext.Books.TryGetValue(id, out Book? book);
      return book;
   }

   public void Add(Book book) {
      dataContext.Books.Add(book.Id, book);
   }
   
   public void Delete(Book book) {
      dataContext.Books.Remove(book.Id);
   }
}