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
using MongoDB.Driver.Core;
using MongoDB.Driver.Core.Operations;

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

        public async Task<IHttpActionResult> Get()
        {
            var categories = await context.Categories.FindAsync(new BsonDocument());

            return Ok(categories.ToList());
        }

        public async Task<IHttpActionResult> Get(int id)
        {
            var idFilter = Builders<Category>.Filter.Eq(c => c.CategoryId, id);

            var category = await context.Categories.FindAsync(idFilter);

            return Ok(category.FirstOrDefault());
        }

        //[System.Web.Http.Route("api/categories/{name}")]
        //public async Task<IHttpActionResult> GetByName(string name)
        //{
        //    //var nameFilter = Builders<Category>.Filter.Where(c => c.CategoryName == name);
        //    var nameFilter = Builders<Category>.Filter.Eq(c => c.CategoryName, name);

        //    var categoris = await context.Categories
        //                                    .Find(nameFilter)
        //                                    //.Sort(Builders<Category>.Sort.Ascending(c => c.CategoryName == name))
        //                                    .ToListAsync();

        //    return Ok(categoris);
        //}

        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> Post(Category category)
        {
            await context.Categories.InsertOneAsync(category);

            var location = $"{this.ActionContext.Request.RequestUri.AbsoluteUri}/{category.CategoryId}";

            return Created(location, category);
        }

    }
}
