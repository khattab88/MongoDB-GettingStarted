using MongoDB.Bson;
using MongoDB.Driver;
using Providers.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Providers
{
    public interface IMongoDbProvider
    {
        void Init(string dbName);
        IMongoClient CreateClient(string connStr);
        IMongoDatabase GetDatabase(string dbName);

        IMongoCollection<T> GetCollection<T>(string collName);

        ServerInfo GetServerInfo();

        void AddToCollection<T>(IMongoCollection<T> collection, T document);

        IEnumerable<T> GetDocuments<T>(IMongoCollection<T> collection);

        T GetDocumentById<T>(string id, IMongoCollection<T> collection);

        /// <summary>
        /// Updates document (replace)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="document"></param>
        /// <param name="collection"></param>
        
        void EditDocument<T>(string id, T document, IMongoCollection<T> collection);
        /// <summary>
        /// Updates document (partial update, no replacement)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="updates"></param>
        /// <param name="collection"></param>
        void EditDocument<T>(string id, IDictionary<string,object> updates, IMongoCollection<T> collection);

        void RemoveDocument<T>(string id, IMongoCollection<T> collection);
    }
}
