using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using Providers.Model;
using Providers.Exceptions;
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

        public IMongoCollection<T> GetCollection<T>(string collName)
        {
            try
            {
                var collection = _db.GetCollection<T>(collName);

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

        public void AddToCollection<T>(IMongoCollection<T> collection, T document)
        {
            collection.InsertOne(document);
        }

        public IEnumerable<T> GetDocuments<T>(IMongoCollection<T> collection)
        {
            var documents = collection
                            .Find(FilterDefinition<T>.Empty)
                            .ToList<T>();

            return documents;
        }

        public T GetDocumentById<T>(string id, IMongoCollection<T> collection)
        {
            var objectId = new ObjectId(id);
            var idFilter = Builders<T>.Filter.Eq("_id", objectId);

            var doc = collection.Find(idFilter).SingleOrDefault();
            if (doc == null)
                throw new DocumentNotFoundException(id, collection.CollectionNamespace.CollectionName);

            return doc;
        }

        public void EditDocument<T>(string id, T document, IMongoCollection<T> collection)
        {
            var objectId = new ObjectId(id);
            var idFilter = Builders<T>.Filter.Eq("_id", objectId);

            // if there is no existing document, throws not found excception
            var oldDocument = GetDocumentById(id, collection);

            collection.ReplaceOne(idFilter, document);
        }

        public void EditDocument<T>(string id, IDictionary<string, object> updates, IMongoCollection<T> collection)
        {
            var objectId = new ObjectId(id);
            var idFilter = Builders<T>.Filter.Eq("_id", objectId);

            // if there is no existing document, throws not found excception
            var oldDocument = GetDocumentById(id, collection);

            //var partialUpdate = Builders<T>.Update.Set(d => d.)
        }

        public void RemoveDocument<T>(string id, IMongoCollection<T> collection)
        {
            var objectId = new ObjectId(id);
            var idFilter = Builders<T>.Filter.Eq("_id", objectId);

            collection.DeleteOne(idFilter);
        }
    }
}