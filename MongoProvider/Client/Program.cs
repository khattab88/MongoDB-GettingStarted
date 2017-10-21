using Client.Model;
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

        static void OldMain(string[] args)
        {

            #region Provider

            string connStr = Settings.Default.MongoConnStr,
                   dbName = Settings.Default.MongoDbName;


            _mongo = container.Resolve<IMongoClient>();
            _provider = new MongoDbProvider(connStr, _mongo);



            //// get client
            //var client = _provider.CreateClient(connStr);


            //// get database
            //var db = _provider.GetDatabase("test");

            //// set working database
            //_provider.Init(dbName);


            //// get collection
            //var products = _provider.GetCollection<BsonDocument>("products");
            //var categories = _provider.GetCollection<BsonDocument>("categories");


            //// add document
            //var category = new Category
            //{
            //    CategoryId = 3,
            //    CategoryName = "Accessories"
            //};
            //_provider.AddToCollection<Category>(categories, category);


            //// get all documents
            //var docs = _provider.GetDocuments<BsonDocument>(categories);
            //foreach (var item in docs)
            //{
            //    Console.WriteLine(item.ToJson());
            //}


            //// get document by id (ObjectId("59e49ada5c9c8fc0039c4c0e"))
            //var doc = _provider.GetDocumentById("59e49ada5c9c8fc0039c4c0e", products);
            //Console.WriteLine(doc["_id"]);


            //// upadte document (category with ObjectId("59ea0f2fd1cd314b568a47a7"))
            //var upadated = new Category
            //{
            //    CategoryId = 3,
            //    CategoryName = "Games"
            //}.ToBsonDocument();
            //_provider.EditDocument("59ea0f2fd1cd314b568a47a7", upadated, categories);


            //// partial update
            //var updates = Builders<Category>.Update.Set(d => d.CategoryName, 5000);
            //_provider.EditDocument("59ea0f2fd1cd314b568a47a7", updates, categories);


            //// delete document
            //_provider.RemoveDocument("59ea5355c09c243ab0ac5680", categories);

            #endregion

        }

        static void Main()
        {
            #region Context

            var pos = new PosContext(Settings.Default.MongoConnStr,
                                     Settings.Default.MongoDbName);

            var categories = pos.Categories.Find(new BsonDocument()).ToList();
            Console.WriteLine(categories.Count);


            #endregion

        }

        
    }
}
