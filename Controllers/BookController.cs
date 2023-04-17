using Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    public class BookController
    {
        public Book CreateBook(string t, string isbn, string p, List<Author> a) 
        {
            Book b = new Book();

            b.Id = ObjectId.GenerateNewId().ToString();
            b.Title = t;
            b.ISBN = isbn;
            b.Publisher = p;
            b.Authors = new AuthorController().CreateAuthorList(a);

            return b;
        }
        public void InsertBook(Book b, IMongoCollection<Book> c)
        {
            c.InsertOne(b);
        }
        public Book SelectBook(string t, IMongoCollection<Book> s)
        {
            var filter = Builders<Book>.Filter.Regex("Title", t);

            var result = s.Find(filter).FirstOrDefault();

            return result;
        }
        public Book UpdateBookByTitle(Book b, IMongoCollection<Book> s)
        {
            var filter = Builders<Book>.Filter.Regex("Title", b.Title);
            var update = Builders<Book>.Update.Set("Title", b.Title);

            s.UpdateOne(filter, update);

            return b = SelectBook(b.Title, s);
        }
        public Book UpdateBookByISBN(Book b, IMongoCollection<Book> s)
        {
            var filter = Builders<Book>.Filter.Regex("ISBN", b.ISBN);
            var update = Builders<Book>.Update.Set("ISBN", b.ISBN);

            s.UpdateOne(filter, update);

            return b = SelectBook(b.Title, s);
        }
        public Book UpdateBookByPublisher(Book b, IMongoCollection<Book> s)
        {
            var filter = Builders<Book>.Filter.Regex("Publisher", b.Publisher);
            var update = Builders<Book>.Update.Set("Publisher", b.Publisher);

            s.UpdateOne(filter, update);

            return b = SelectBook(b.Title, s);
        }
        public Book UpdateBookByAuthors(Book b, IMongoCollection<Book> s)
        {

            var filter = Builders<Book>.Filter.Regex("Authors", b.Publisher);
            var update = Builders<Book>.Update.Set("Publisher", b.Publisher);

            s.UpdateOne(filter, update);

            return b = SelectBook(b.Title, s);
        }
        public void ListBooks(IMongoCollection<Book> s)
        {
            var shelf = s.Find(book => true).ToList();

            foreach (var book in shelf)
            {
                Console.WriteLine(book.ToString());
            }
        }
    }
}
