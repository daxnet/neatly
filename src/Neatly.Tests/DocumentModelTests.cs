using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neatly.DocumentModel;
using Newtonsoft.Json;

namespace Neatly.Tests
{
    [TestClass]
    public class DocumentModelTests
    {
        private static readonly JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
            Formatting = Formatting.Indented,
            ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
            PreserveReferencesHandling = PreserveReferencesHandling.Objects
        };

        private static Document CreateMockDocument()
        {
            var doc = new Document { Title = "title", Author = "abc" };
            var chap1 = new DocumentNode(doc, "Chapter 1", "Content 1");
            var chap2 = new DocumentNode(doc, "Chapter 2", "Content 2");
            var chap21 = new DocumentNode(chap2, "Chapter 2.1", "Content 2.1");

            return doc;
        }

        [TestMethod]
        public void SerializationTest()
        {
            var doc = CreateMockDocument();
            var json = JsonConvert.SerializeObject(doc, jsonSerializerSettings);
            Assert.IsNotNull(json);
        }

        [TestMethod]
        public void DeserializeTest()
        {
            var doc = CreateMockDocument();
            var json = JsonConvert.SerializeObject(doc, jsonSerializerSettings);
            var doc2 = JsonConvert.DeserializeObject<Document>(json);
            Assert.IsNotNull(doc2);
            Assert.AreEqual("title", doc.Title);
        }

        [TestMethod]
        public void SerializeIdTest()
        {
            var doc = CreateMockDocument();
            var g1 = doc.Id;
            var g2 = doc.ChildNodes.First().Id;
            var json = JsonConvert.SerializeObject(doc, jsonSerializerSettings);
            var doc2 = JsonConvert.DeserializeObject<Document>(json);
            Assert.AreEqual(g1, doc2.Id);
            Assert.AreEqual(g2, doc2.ChildNodes.First().Id);
        }

        [TestMethod]
        public void ChangeDocumentPropertyEventTest()
        {
            var counter = 0;
            var doc = new Document("Help Document");
            Assert.AreEqual("Help Document", doc.Title);

            doc.PropertyChanged += (s, e) =>
              {
                  counter++;
              };
            doc.Title = "Hello World";
            Assert.AreEqual("Hello World", doc.Title);
            Assert.AreEqual(1, counter);
        }

        [TestMethod]
        public void AddNodeToDocumentEventTest()
        {
            var counter = 0;
            var doc = new Document();
            doc.PropertyChanged += (s, e) =>
              {
                  counter++;
              };
            var chap1 = new DocumentNode(doc, "Chapter 1", "Content 1");
            Assert.AreEqual(1, counter);
        }

        [TestMethod]
        public void ChangeChildPropertyEventTest()
        {
            var counter = 0;
            var doc = new Document();

            doc.PropertyChanged += (s, e) =>
              {
                  counter++;
              };
            var chap1 = new DocumentNode(doc, "Chapter 1", "Content 1");
            Assert.AreEqual("Chapter 1", chap1.Title);
            Assert.AreEqual("Content 1", chap1.Content);

            chap1.Title = "Chap 1";
            Assert.AreEqual("Chap 1", chap1.Title);
            Assert.AreEqual(2, counter);
        }

        [TestMethod]
        public void AddNodeToChildEventTest()
        {
            var counter = 0;
            var doc = new Document();
            doc.PropertyChanged += (s, e) =>
            {
                counter++;
            };

            var chap1 = new DocumentNode(doc, "Chapter 1", "Content 1");
            var chap11 = new DocumentNode(chap1, "Chapter 11", "Content 11");

            Assert.AreEqual(2, counter);
        }

        [TestMethod]
        public void ChangeGrandChildPropertyEventTest()
        {
            var counter = 0;
            var doc = new Document();
            doc.PropertyChanged += (s, e) =>
            {
                counter++;
            };

            var chap1 = new DocumentNode(doc, "Chapter 1", "Content 1");
            var chap11 = new DocumentNode(chap1, "Chapter 11", "Content 11");
            Assert.AreEqual(2, counter);
            chap11.Title = "Chpater 111";
            Assert.AreEqual(3, counter);
        }

        [TestMethod]
        public void RemoveChildEventTest()
        {
            var counter = 0;
            var doc = new Document();
            doc.PropertyChanged += (s, e) =>
            {
                counter++;
            };

            var chap1 = new DocumentNode(doc, "Chapter 1", "Content 1");
            Assert.AreEqual(1, counter);

            counter = 0;
            doc.Remove(chap1);
            Assert.AreEqual(1, counter);
        }
    }
}
