using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class PocoTests
    {
        class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public List<string> Friends { get; set; }
            public Contact Contact { get; set; }
            [BsonIgnore]
            public string Title { get; set; }

            [BsonIgnoreIfNull]
            [BsonElement("Nationality")]
            public string Country { get; set; }

            [BsonRepresentation(BsonType.Double, AllowTruncation =true)]
            public decimal Salary { get; set; }
        }

        class Contact
        {
            public string Email { get; set; }
            public string Phone { get; set; }
        }

        public PocoTests()
        {
            JsonWriterSettings.Defaults.Indent = true;
        }

        [TestMethod]
        public void AutomaticSerialization()
        {
            var person = new Person
            {
                Name = "adam",
                Age = 15,
                Friends = new List<string> { "jane", "michael", "howard" },
                Contact = new Contact
                {
                    Email = "adam@domain.com",
                    Phone = "0123456"
                }
            };

            

            //var props = new Dictionary<string, object>();
            //foreach (var prop in person.GetType().GetProperties())
            //{
            //    props.Add(prop.Name, prop.GetValue(person));
            //}

            //var document = new BsonDocument(props);

            Console.WriteLine(person.ToJson());

        }

        [TestMethod]
        public void IgnoreSerializationAttribute()
        {
            var person = new Person
            {
                Name = "tom",
                Title = "Mr."
            };

            Console.WriteLine(person.ToJson());
        }

        [TestMethod]
        public void RenameSerializationAttribute()
        {
            var person = new Person
            {
                Country = "Sweden"
            };

            Console.WriteLine(person.ToJson());
        }

        [TestMethod]
        public void IgnoreIfNullAttribute()
        {
            var person = new Person
            {
                Name = "lewis",
                Country = null
            };

            Console.WriteLine(person.ToJson());
        }

        [TestMethod]
        public void BsonRepresentationAttribute()
        {
            var person = new Person
            {
                Salary = 3000.5487m
            };

            Console.WriteLine(person.ToJson());
        }

    }
}
