using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Providers;
using MongoDB.Driver;
using Tests.Mocks;
using Moq;
using Microsoft.Practices.Unity;
using MongoDB.Bson;

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

            _provider = new MongoDbProvider(_connStr, _mockMongo);
        }

        [Ignore]
        [TestMethod]
        public void CreateClient_ReturnsValidMongoClient()
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
            string dbName = "db name",
                   collectionName = "collection name";
            _provider = new MongoDbProvider(_connStr, _mockMongo);
            _provider.Init(dbName);


            // act
            var collection = _provider.GetCollection(collectionName);

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

        [TestMethod]
        public void Init_InitializeWorkingDatabase()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void GetServerInfo_ReturnsValidInfo()
        {
            // arrange
            string appName = "app name",
                   server = $"{_host}:{_port}";

            var connectionMode = ConnectionMode.Automatic;
            int connectionTimeout = 3000;   
            bool useSSL = false;

            // act
            var info = _provider.GetServerInfo();

            // assert
            Assert.IsNotNull(info);
            Assert.AreEqual(appName, info.AppName);
            Assert.AreEqual(connectionMode, info.ConnectionMode);
            Assert.AreEqual(connectionTimeout, info.ConnectionTimeout);
            Assert.AreEqual(server, info.Server);
            Assert.AreEqual(useSSL, info.UseSSL);
        }

        [TestMethod]
        public void AddToCollection_ValidDocument_InsertNewDocument()
        {
            string collctionName = "collection name";
            var document = new BsonDocument();

            _provider.AddToCollection(collctionName, document);

            Assert.IsTrue(document["_id"].IsObjectId);
        }
    }
}
