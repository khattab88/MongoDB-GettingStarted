using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Providers
{
    public abstract class MongoDbContext : IMongoDbContext
    {
        public String ConnectionString { get; set; }
        public IMongoClient Client { get; set; }
        public IMongoDatabase Database { get; set; }

        public MongoDbContext(string connectioString, string database)
        {
            ConnectionString = connectioString;

            Client = new MongoClient(connectioString);
            Database = Client.GetDatabase(database);
        }
    }
}
