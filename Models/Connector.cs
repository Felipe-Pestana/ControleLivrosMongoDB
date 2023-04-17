using MongoDB.Driver;

namespace Models
{
    public class Connector : MongoClient
    {
        public readonly string server = "mongodb://localhost:27017";
        
        private string _database = "ControleLivros";
        
        private string _shelfStored = "Stored";
        private string _shelfBorrowed = "Borrowed";
        private string _shelfReading = "Reading";

        public string GetServer()
        {
            return server;
        }
        public string GetDatabase()
        {
            return _database;
        }
        public string GetshelfStored()
        {
            return _shelfStored;
        }
        public string GetBorrowed()
        {
            return _shelfBorrowed;
        }
        public string GetReading()
        {
            return _shelfReading;
        }

        public Connector()
        {
           throw new NotImplementedException();
        }
    }
}