using MongoDB.Driver;
using Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class PosContext : MongoDbContext
    {
        public PosContext(string connectionString, string database) 
            : base(connectionString, database)
        {
        }

        public IMongoCollection<Category> Categories
        {
            get
            {
                return Database.GetCollection<Category>("categories");
            }
        }

        public IMongoCollection<Product> Product
        {
            get
            {
                return Database.GetCollection<Product>("product");
            }
        }
    }
}
