using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Bson.IO;
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
            var person = new BsonDocument();

            person.Add("name", new BsonString("ali"));
            person.Add("age", new BsonInt32(30));
            person.Add("adult", new BsonBoolean(true));

            Console.WriteLine(person);
            //Assert.AreEqual(person.Elements["name"])
        }

    }
}
