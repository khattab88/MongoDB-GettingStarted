using Api.Properties;
using Model;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Api.Controllers
{
    public class CategoriesController : ApiController
    {
        private readonly PosContext context;

        public CategoriesController()
        {
            context = new PosContext(Settings.Default.ConnectionString,
                                     Settings.Default.Database);
        }

        public async Task<IEnumerable<Category>> Get()
        {
            var categories = await context.Categories.FindAsync(new BsonDocument());

            return categories.ToList();
        }

    }
}
