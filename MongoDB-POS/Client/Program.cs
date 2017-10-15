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
            var collection = db.GetCollection<BsonDocument>("users");

            //var document = new BsonDocument();
            //document.Add(new BsonElement("name", "ahmed"));
            //collection.InsertOne(document);

            var document = collection.Find(new BsonDocument()).FirstOrDefault();
            Console.WriteLine(document);

            //var count = collection.Count(new BsonDocument());
            //Console.WriteLine(count);
        }
    }
}
