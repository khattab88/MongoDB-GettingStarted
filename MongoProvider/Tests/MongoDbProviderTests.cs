using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Providers;
using MongoDB.Driver;
using Tests.Mocks;
using Moq;
using Microsoft.Practices.Unity;

namespace Tests
{
    [TestClass]
    public class MongoDbProviderTests
    {
        IUnityContainer container;

        // SUT
        IMongoDbProvider _provider;

        // dependencies
        IMongoClient _mockMongo;

        string _host;
        int _port;
        string _connStr;

        [TestInitialize]
        public void TestInit()
        {
            container = new UnityContainer();
            container.RegisterType<IMongoClient, MockMongoClient>();

            _host = "host";
            _port = 11111;
            _connStr = string.Format("mongodb://{0}:{1}", _host, _port);

            //_mockMongo = new MockMongoClient();
            _mockMongo = container.Resolve<IMongoClient>();

            //var mockMongo = new Mock<IMongoClient>();
            //mockMongo.Setup(r => r.Settings.Server)
            //         .Returns(new MongoServerAddress(_host, _port));
            //_mockMongo = mockMongo.Object;
 
        }

        [Ignore]
        [TestMethod]
        public void createClient_ReturnsValidMongoClient()
        {
            // arrange
            _provider = new MongoDbProvider(_connStr, _mockMongo);

            // act
            var client = _provider.CreateClient(_connStr);

            // assert
            Assert.AreEqual(client.Settings.Server.Host, _host);
            Assert.AreEqual(client.Settings.Server.Port, _port);
        }

        [Ignore]
        [TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        public void CraeteClient_InvalidConnStr_ThrowsInvalidArgumentException()
        {
            // arrange 
            var invalidConnStr = "invalid conn str";
            _provider = new MongoDbProvider(invalidConnStr, _mockMongo);

            // act
           var client = _provider.CreateClient(invalidConnStr);

            // assert
            Assert.IsNull(client);

        }

        [TestMethod]
        public void GetDatabase_ReturnsDatabase()
        {
            // arrange
            _provider = new MongoDbProvider(_connStr, _mockMongo);
            var dbName = "db name";

            // act 
            var db = _provider.GetDatabase(dbName);

            // assert
            Assert.IsNotNull(db);
            Assert.AreEqual(db.DatabaseNamespace.DatabaseName, dbName);
        }

        [TestMethod]
        public void GetCollection_ReturnsValidCollection()
        {
            // arrange
            _provider = new MongoDbProvider(_connStr, _mockMongo);
            string dbName = "db name",
                   collectionName = "collection name";

            // act
            var collection = _provider.GetCollection(dbName, collectionName);

            // assert
            Assert.IsNotNull(collection);
            Assert.AreEqual(collectionName, collection.CollectionNamespace.CollectionName);
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
