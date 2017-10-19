using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class BsonDocumentTests
    {
        public BsonDocumentTests()
        {
            JsonWriterSettings.Defaults.Indent = true;
        }

        [TestMethod]
        public void CreateEmptyDocument()
        {
            var document = new BsonDocument();

            Console.WriteLine(document);
        }

        [TestMethod]
        public void AddElementToDocument()
        {
            //var person = new BsonDocument();
            //person.Add("name", new BsonString("ali"));
            //person.Add("age", new BsonInt32(30));
            //person.Add("isAdult", new BsonBoolean(true));

            // using object initializer syntax
            var person = new BsonDocument
            {
                { "name", "ali" },
                { "age", 30 },
                { "isAdult", true }
            };

            Console.WriteLine(person);
            //Assert.AreEqual(person.Elements["name"])
        }

        [TestMethod]
        public void AddArrayToDocument()
        {
            var person = new BsonDocument();

            person.Add("friends",
                        new BsonArray(new[]
                            { "john", "ron", "steve" }));

            Console.WriteLine(person);
        }

        [TestMethod]
        public void EmbedDocument()
        {
            //var person = new BsonDocument();
            //person.Add("address",
            //    new BsonDocument {
            //        { "city", "london" },
            //        { "street", "downing st." },
            //        { "building", 55 }
            //    });

            var person = new BsonDocument
            {
                { "id", 1 },
                { "address", new BsonDocument
                    {
                        { "city", "london" },
                        { "street", "downing st." },
                        { "building", 55 }
                    }
                }

            };

            Console.WriteLine(person);

        }

        [TestMethod]
        public void ConvertBsonValues()
        {
            var person = new BsonDocument
            {
                { "age", 30 }
            };

            Console.WriteLine(person["age"]);

            Assert.AreEqual(30, person["age"]);
            Assert.IsTrue(person["age"].IsInt32);
            Assert.IsFalse(person["age"].IsInt64);
        }

        [TestMethod]
        public void ToBson()
        {
            var person = new BsonDocument
            {
                { "name", "craig" }
            };

            var bson = BitConverter.ToString(person.ToBson());
            Console.WriteLine(bson);

            //var deserializedPerson = BsonSerializer.Deserialize<BsonDocument>(bson);
            //Console.WriteLine(deserializedPerson);
        }
        
    }
}
