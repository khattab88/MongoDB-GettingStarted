using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Providers
{
    public interface IDbProvider
    {
        IMongoClient CreateClient(string connStr);
        IMongoDatabase GetDatabase(string dbName);
    }
}
