using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text;

namespace Models
{
    public class Book
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public List<Author> Authors { get; set; } = new();

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"\n\nTítulo: {Title}\n");
            sb.Append($"ISBN:{ISBN} \n");
            sb.Append($"Editora: {Publisher}\n");
            sb.Append("Autor(es):\n");
            foreach (var author in Authors)
            {
                sb.Append(author.ToString());
            }
            return sb.ToString();
        }
    }
}