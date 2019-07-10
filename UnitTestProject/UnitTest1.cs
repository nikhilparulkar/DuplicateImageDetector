using NUnit.Framework;
using DuplicateImageDetector.Service;
using Microsoft.Extensions.Logging;
using NLog;
using Moq;
using System.IO;
using System;
using Microsoft.Extensions.Logging.Abstractions;

namespace Tests
{
    public class Tests
    {
       

        [SetUp]
        public void Setup()
        {
            
            
        }

        [Test]
        //where files are duplicate 
        public void Test1()
        {
            string[] original = { "1.jpg", "2.jpg", "4.jpg", "5.jpg", "6.jpg" };
            string[] duplicate = { "3.jpg","7.jpg"};
            var logger= new NullLogger<ImageDuplicateDetectorService>();
            var svc = new ImageDuplicateDetectorService(logger);
            var result = svc.ImageDuplicates(System.IO.Path.GetFullPath(@"..\..\..\") + "TestData1");

            Assert.AreEqual(original.Length, result.Original.Count);
            Assert.AreEqual(duplicate.Length, result.duplicates.Count);

            
        }

        [Test]
        // Where image is triplicated with different names
        public void Test2()
        {
            string[] original = { "1.jpg", "4.jpg", "5.jpg", "6.jpg" };
            string[] duplicate = { "3.jpg", "7.jpg","9.jpg" };
            var logger = new NullLogger<ImageDuplicateDetectorService>();
            var svc = new ImageDuplicateDetectorService(logger);
            var result = svc.ImageDuplicates(System.IO.Path.GetFullPath(@"..\..\..\") + "TestData2");

            Assert.AreEqual(original.Length, result.Original.Count);
            Assert.AreEqual(duplicate.Length, result.duplicates.Count);


        }


        [Test]
        // Where Image path is incorrect
        public void Test3()
        {
            
            var logger = new NullLogger<ImageDuplicateDetectorService>();
            var svc = new ImageDuplicateDetectorService(logger);
            var result = svc.ImageDuplicates(System.IO.Path.GetFullPath(@"..\..\..\") + "TestData3");

            Assert.AreEqual(string.IsNullOrWhiteSpace (result.Error), false);
            


        }
    }

    
}