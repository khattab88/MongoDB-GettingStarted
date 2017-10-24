using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Product
    {
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string _id { get; }

        [BsonElement("productId")]
        public int productId { get; set; }

        [BsonElement("productName")]
        public string ProductName { get; set; }

        [BsonElement("productCode")]
        public string ProductCode { get; set; }

        [BsonElement("releaseDate")]
        public DateTime ReleaseDate { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("price")]
        public decimal Price { get; set; }

        [BsonElement("starRating")]
        public double StarRating { get; set; }

        [BsonElement("imageUrl")]
        public string ImageUrl { get; set; }

        [BsonElement("categoryId")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [BsonElement("tags")]
        public List<string> Tags = new List<string>();
    }
}
