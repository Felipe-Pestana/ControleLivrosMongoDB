using Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    public class ShelfController
    {
        public Shelf CreateShelf(string n)
        {

            Shelf s = new Shelf();

            s.Name = n;

            return s;
        }

        
    }
}
