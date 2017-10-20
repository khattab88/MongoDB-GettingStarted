using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public double StarRating { get; set; }
        public string ImageUrl { get; set; }

        public Category Category { get; set; }
        public List<string> Tags = new List<string>();
    }
}
