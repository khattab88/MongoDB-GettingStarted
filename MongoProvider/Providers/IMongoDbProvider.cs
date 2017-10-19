using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Providers
{
    public interface IMongoDbProvider
    {
        IMongoClient CreateClient(string connStr);
        IMongoDatabase GetDatabase(string dbName);

        IMongoCollection<BsonDocument> GetCollection(string dbName , string collName);
    }
}
