﻿using Api.Properties;
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
            var filterById = Builders<Category>.Filter.Eq(c => c.CategoryId, id);

            var categories = await context.Categories
                                    .FindAsync(filterById);

            return Ok(categories.ToList());
        }

    }
}
