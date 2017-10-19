using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using Providers.Model;
//using Microsoft.Practices.Unity;

namespace Providers
{
    public class MongoDbProvider : IMongoDbProvider
    {
        private readonly string _connStr;
        private readonly IMongoClient _dbClient;
        private IMongoDatabase _db;

        //[InjectionConstructor]
        public MongoDbProvider(string connStr ,IMongoClient Client)
        {
            this._connStr = connStr;
            this._dbClient = Client;
            
        }

        /// <summary>
        /// initialize working database
        /// </summary>
        /// <param name="dbName">database name</param>
        public void Init(string dbName)
        {
            this._db = _dbClient.GetDatabase(dbName);
        }

        IMongoClient IMongoDbProvider.CreateClient(string connStr)
        {
            try
            {
                return _dbClient;
            }
            catch (Exception)
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

        public IMongoCollection<BsonDocument> GetCollection(string collName)
        {
            try
            {
                var collection = _db.GetCollection<BsonDocument>(collName);

                return collection;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ServerInfo GetServerInfo()
        {
            var info = new ServerInfo
            {
                AppName = _dbClient.Settings.ApplicationName,
                Server = $"{_dbClient.Settings.Server.Host}:{_dbClient.Settings.Server.Port}",
                ConnectionMode = _dbClient.Settings.ConnectionMode,
                ConnectionTimeout = (int)_dbClient.Settings.ConnectTimeout.TotalMilliseconds,
                UseSSL = _dbClient.Settings.UseSsl
            };
            
            return info;
        }
    }
}