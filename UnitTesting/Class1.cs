using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BlobStorageDemo;
using Moq;
using NUnit;
using NUnit.Framework;

namespace UnitTesting
{   
    [TestFixture]
    public class Class1
    {
        [Test]
        public async Task IsImageExists_IfNotExists_ReturnsNULL()
        {
            //Arrange
            ImageService imageService = new ImageService();
            HttpPostedFileBase httpPostedFile = Mock.Of<HttpPostedFileBase>();
            var mock = Mock.Get(httpPostedFile);
            mock.Setup(_ => _.FileName).Returns("Movie.jpg");

            //Act
            var result = await imageService.IsImageExists(httpPostedFile);

            //Assert

            Assert.AreEqual(null, result);
            
        }
 
        [Test]
        public async Task UploadImageAsync_WrongExtension_ReturnsNull()
        {
            //Arrange
            ImageService imageService = new ImageService();
            HttpPostedFileBase httpPostedFile = Mock.Of<HttpPostedFileBase>();
            var mock = Mock.Get(httpPostedFile);
            mock.Setup(_ => _.FileName).Returns("abc.txt");

            //Act
            var result = await imageService.UploadImageAsync(httpPostedFile);

            //Assert

            Assert.AreEqual(null, result);


        }
    }
}
