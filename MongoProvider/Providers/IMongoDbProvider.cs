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
    }
}
