using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Shelf
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Book> Books { get; set; }

        public override string ToString()
        {
            return $"Estante: {Name}\n";
        }
    }
}
