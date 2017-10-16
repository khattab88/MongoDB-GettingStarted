using Microsoft.Practices.Unity;
using MongoDB.Driver;
using Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public static class Bootstrapper
    {
        public static void Run()
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<IMongoClient, MongoClient>();
            container.RegisterType<IDbProvider, MongoDbProvider>();
        }
    }
}
