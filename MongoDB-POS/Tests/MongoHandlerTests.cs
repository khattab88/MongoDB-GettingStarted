using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class MongoHandlerTests
    {
        [TestMethod]
        public void createClient_ReturnsValidMongoClient()
        {
            // arrange
            MongoDBProvider provider = new MongoDBProvider();
            string connStr = "connection string";

            // act
            var client = provider.createClient(connStr);

            // assert
            Assert.AreEqual(connStr, client.ConnectionString);
            Assert.IsTrue(client.IsConnected);
        }

        [TestMethod]
        public void GetDatabase_ReturnsDatabase()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void GetCollection_ReturnsValidCollection()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void GetCollectionCount_ReturnsValidCollectionCount()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void InsertDocument_ReturnsNewlyCreatedDocument()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void GetFirstDocument_ReturnsFirstDocumentInCollection()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void UpdateDocument_ReturnsUpdatedDocument()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void DeleteDocument_ReturnsValidCollectionCount()
        {
            throw new NotImplementedException();
        }

    }
}
