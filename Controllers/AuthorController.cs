using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using MongoDB.Bson;

namespace Controllers
{
    public class AuthorController
    {
        public List<Author> CreateAuthorList(List<Author> a) 
        {
            foreach (var author in a)
            {
                author.Id = ObjectId.GenerateNewId().ToString();
            }

            return a;
        }
        public Author CreateAuthor(string n, string s)
        {
            Author author = new Author();
            author.Name = n;
            author.Surname = s;

            return author;
        }

    }
}
