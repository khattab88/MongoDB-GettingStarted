using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Providers;
using MongoDB.Driver;
using Tests.Mocks;

namespace Tests
{
    [TestClass]
    public class MongoDbProviderTests
    {
        // SUT
        IDbProvider provider;

        // dependencies
        IMongoClient mockMongo;
        string host;
        int port;
        string connStr;

        [TestInitialize]
        public void TestInit()
        {
            host = "host";
            port = 11111;
            connStr = string.Format("mongodb://{0}:{1}", host, port);

            mockMongo = new MockMongoClient();

            provider = new MongoDbProvider(connStr, mockMongo);   
        }

        [Ignore]
        [TestMethod]
        public void createClient_ReturnsValidMongoClient()
        {
            // act
            var client = provider.CreateClient(connStr);

            // assert
            Assert.AreEqual(client.Settings.Server.Host, host);
            Assert.AreEqual(client.Settings.Server.Port, port);
        }

        [TestMethod]
        public void GetDatabase_ReturnsDatabase()
        {
            // arrange
            var dbName = "db name";

            // act 
            var db = provider.GetDatabase(dbName);

            // assert
            Assert.IsNotNull(db);
            Assert.AreEqual(db.DatabaseNamespace.DatabaseName, dbName);
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
