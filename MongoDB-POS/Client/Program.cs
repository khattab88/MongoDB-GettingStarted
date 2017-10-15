using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello MongoDB !");

            var client = new MongoClient("mongodb://localhost:27017");
            var db = client.GetDatabase("test");
            var users = db.GetCollection<BsonDocument>("users");

            var usersCount = users.Count(new BsonDocument());
            Console.WriteLine(usersCount);
        }
    }
}
