using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Providers
{
    public interface IMongoDbContext
    {
        string ConnectionString { get; set; }
        IMongoClient Client { get; set; }
        IMongoDatabase Database { get; set; }
    }
}
