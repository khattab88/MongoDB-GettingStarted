using Api.Properties;
using Model;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using MongoDB.Driver;

namespace Api.Controllers
{
    public class ProductsController : ApiController
    {
        private readonly PosContext _context;

        public ProductsController()
        {
            _context = new PosContext(Settings.Default.ConnectionString,
                                     Settings.Default.Database);
        }

        public async Task<IHttpActionResult> Get()
        {
            var products = await _context.Products.FindAsync(new BsonDocument());

            return Ok(products.ToList());
        }
    }
}
