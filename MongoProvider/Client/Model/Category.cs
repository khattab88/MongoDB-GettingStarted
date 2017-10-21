using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model
{
    public class Category
    {
        //[BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public ObjectId id { get; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
