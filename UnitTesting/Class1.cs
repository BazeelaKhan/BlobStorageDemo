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
          
            byte[] byteBuffer = new Byte[10];
            Random rnd = new Random();
            rnd.NextBytes(byteBuffer);
            System.IO.MemoryStream testStream = new System.IO.MemoryStream(byteBuffer);
            var TestImageFile = new MyTestPostedFileBase(testStream, "images/png", "test-file.png");
           
            //Act
            var result = await imageService.IsImageExists(TestImageFile);

            //Assert

            Assert.AreEqual(null, result);

        }

        [Test]
        public void CheckExtension_CorrectExtension_returnsTrue()
        {
            //Arrange 
            ImageService imageService = new ImageService();
            
            string fileName = @"/Users/Bazeela_Khan/Downloads/Books.jpg";
            string FileExtension = fileName.Substring(fileName.LastIndexOf('.') + 1).ToLower();
       
            //Act 
            var result =  imageService.CheckExtension(FileExtension);

            //Assert 
            Assert.AreEqual(true,result);

        }
        [Test]
        public void CheckExtension_CorrectExtension_returnsFalse()
        {
            //Arrange 
            ImageService imageService = new ImageService();
            string fileName = @"/Users/Bazeela_Khan/Downloads/DemoTest.txt";
            string FileExtension = fileName.Substring(fileName.LastIndexOf('.') + 1).ToLower();

            //Act 
            var result = imageService.CheckExtension(FileExtension);

            //Assert 
            Assert.AreEqual(false, result);

        }
    }
}
