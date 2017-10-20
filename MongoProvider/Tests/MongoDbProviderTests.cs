using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Providers;
using MongoDB.Driver;
using Tests.Mocks;
using Moq;
using Microsoft.Practices.Unity;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using Providers.Exceptions;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class MongoDbProviderTests
    {
        public class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }

        IUnityContainer container;

        // SUT
        IMongoDbProvider _provider;

        // dependencies
        IMongoClient _mockMongo;

        string _host;
        int _port;
        string _connStr;
        IMongoCollection<BsonDocument> _collection;

        [TestInitialize]
        public void TestInit()
        {
            JsonWriterSettings.Defaults.Indent = true;

            container = new UnityContainer();
            container.RegisterType<IMongoClient, MockMongoClient>();

            _host = "host";
            _port = 11111;
            _connStr = string.Format("mongodb://{0}:{1}", _host, _port);
            _collection = new MockMongoCollection<BsonDocument>();

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
            var collection = _provider.GetCollection<BsonDocument>(collectionName);

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
            var collection = new MockMongoCollection<Person>();
            var person = new Person
            {
                Name = "john",
                Age = 20
            };

            _provider.AddToCollection<Person>(collection, person);

            Console.WriteLine(person.ToJson());
        }

        [TestMethod]
        public void GetDocuments_GetAllDocuments_RetunsDocumentsInCollection()
        {
            var collection = new MockMongoCollection<Person>();

            var documents = _provider.GetDocuments(collection);

            Assert.IsNotNull(documents);
        }

        [TestMethod]
        public void GetDocument_GivenDocumentId_ReturnsValidDocument()
        {
            var id = "123";
            var collection = new MockMongoCollection<BsonDocument>();

            var doc = _provider.GetDocumentById(id, collection);

            Assert.IsNotNull(doc);
            Assert.AreEqual(id, doc["_id"].AsString);
        }

        [TestMethod]
        public void GetDocument_InvalidDocumentId_ThrowsDocumentNotFoundException()
        {
            var id = "000";
            var collection = new MockMongoCollection<BsonDocument>();

            _provider.GetDocumentById(id, collection);
        }

        [TestMethod]
        public void EditDocument_UpdatingExsistingDocument()
        {
            // arrange
            var id = "123";
            var document = new Person
            {
                Name = "updated name",
                Age = 50
            }.ToBsonDocument();
            var collection = new MockMongoCollection<BsonDocument>();
            var oldDocument = _provider.GetDocumentById(id, collection);

            // act
            _provider.EditDocument(id, document, collection);

            Assert.AreEqual(oldDocument["_id"].AsString, document["_id"].AsString);
            Assert.AreNotEqual(oldDocument["name"].AsString, document["name"].AsString);
        }

        [TestMethod]
        [ExpectedException(typeof(DocumentNotFoundException))]
        public void EditDocument_InvalidDocumentId_ThrowsDocumentNotFoundException()
        {
            // arrange
            var id = "000";
            var document = new Person
            {
                Name = "updated name",
                Age = 50
            }.ToBsonDocument();
            var collection = new MockMongoCollection<BsonDocument>();

            // act
            _provider.EditDocument(id, document, collection);

        }

        [TestMethod]
        public void EditDocument_PartialUpdate_UpdateDocumentWithoutReplace()
        {
            // arrange
            var id = "123";
            var updates = new Dictionary<string, object>
            {
                { "name", "new name" },
                { "age", 30 }
            };
            var oldDocument = _provider.GetDocumentById(id, _collection);

            // act
            _provider.EditDocument(id, updates, _collection);
            var updatedDocument = _provider.GetDocumentById(id, _collection);

            Assert.AreEqual(oldDocument["_id"].AsObjectId, updatedDocument["_id"].AsObjectId);
            Assert.AreNotEqual(oldDocument["name"].AsString, updatedDocument["name"].AsString);
            Assert.AreNotEqual(oldDocument["age"].AsInt32, updatedDocument["age"].AsInt32);
        }

        [TestMethod]
        public void RemoveDocument_ValidDocumentId_DeleteDocumentFromCollection()
        {
            var id = "123";

            _provider.RemoveDocument(id, _collection);
 
            Assert.Fail();   // assertion needed
        }

    }
}
