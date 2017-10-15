using System;
using MongoDB.Driver;

namespace Providers
{
    public class MongoDbProvider : IDbProvider
    {
        private readonly string _connStr;
        private readonly IMongoClient _dbClient;

        public MongoDbProvider(string connStr ,IMongoClient Client)
        {
            this._connStr = connStr;
            this._dbClient = Client;

            CreateClient(connStr);
        }

        public IMongoClient CreateClient(string connStr)
        {
            return _dbClient;
        }

        public IMongoDatabase GetDatabase(string dbName)
        {
            return _dbClient.GetDatabase(dbName, null);
        }
    }
}