using Client.Properties;
using Microsoft.Practices.Unity;
using MongoDB.Bson;
using MongoDB.Driver;
using Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Program
    {
        private static IUnityContainer container;

        private static IMongoClient _mongo;
        private static IMongoDbProvider _provider;

        static Program()
        {
            container = new UnityContainer();
            container.RegisterType<IMongoClient, MongoClient>(new InjectionConstructor());
            //container.RegisterType<IMongoDbProvider, MongoDbProvider>(new InjectionConstructor());
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello MongoDB !");

            string connStr = Settings.Default.MongoConnStr,
                   dbName = "test";

            //var mongo = new MongoClient();
            _mongo = container.Resolve<IMongoClient>();

             _provider = new MongoDbProvider(connStr, _mongo);
            //_provider = container.Resolve<IMongoDbProvider>();


            // get client
            var client = _provider.CreateClient(connStr);
            Console.WriteLine(client);

            // get database
            var db = _provider.GetDatabase("test");
            Console.WriteLine(db);

            // get collection
            var collection = _provider.GetCollection(dbName, "products");
        


            var doc = collection.Find(new BsonDocument()).FirstOrDefault();

            //var document = new BsonDocument();
            //document.Add(new BsonElement("name", "ahmed"));
            //collection.InsertOne(document);

            //var document = collection.Find(new BsonDocument()).FirstOrDefault();
            //Console.WriteLine(document);

            //var count = collection.Count(new BsonDocument());
            //Console.WriteLine(count);
        }
    }
}
