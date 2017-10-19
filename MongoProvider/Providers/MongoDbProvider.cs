using System;
using MongoDB.Bson;
using MongoDB.Driver;
//using Microsoft.Practices.Unity;

namespace Providers
{
    public class MongoDbProvider : IMongoDbProvider
    {
        private readonly string _connStr;
        private readonly IMongoClient _dbClient;

        //[InjectionConstructor]
        public MongoDbProvider(string connStr ,IMongoClient Client)
        {
            this._connStr = connStr;
            this._dbClient = Client;

            CreateClient(connStr);
        }

        public IMongoClient CreateClient(string connStr)
        {
            try
            {
                return _dbClient;
            }
            catch(Exception)
            {
                return null;
            }
        }


        public IMongoDatabase GetDatabase(string dbName)
        {
            try
            {
                return _dbClient.GetDatabase(dbName, null);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IMongoCollection<BsonDocument> GetCollection(string dbName, string collName)
        {
            try
            {
                var db = GetDatabase(dbName);

                var collection = db.GetCollection<BsonDocument>(collName);

                return collection;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}