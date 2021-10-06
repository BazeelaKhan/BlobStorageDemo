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
        public void Demo_Check_URI()
        {
            ImageService imageService = new ImageService();
          
            string filePath = Path.GetFullPath(@"testfiles\testimage.jpg");
            FileStream fileStream = new FileStream(filePath, FileMode.Open);
            var TestImageFile = new MyTestPostedFileBase(fileStream, "images/jpg", "testimage.jpg");

            var result = imageService.Demo(TestImageFile);


            Assert.AreEqual("http://normal-size/testimage.jpg", result);

        }
        // Not work because of Subscription Issue.
        // See Demo Method to for functionality
        //[Test]
        //public async Task IsImageExists_IfExists_ReturnsURL()
        //{
        //    //Arrange
        //    ImageService imageService = new ImageService();

        //    byte[] byteBuffer = new Byte[10];
        //    Random rnd = new Random();
        //    rnd.NextBytes(byteBuffer);
        //    System.IO.MemoryStream testStream = new System.IO.MemoryStream(byteBuffer);
        //    var TestImageFile = new MyTestPostedFileBase(testStream, "images/png", "TajMahel.jpg");

        //    //Act
        //    var result = await imageService.IsImageExists(TestImageFile);

        //    //Assert

        //    Assert.AreEqual("http://asdfsdf/normal-size/TajMahel.jpg", result);

        //}
        [Test]
        public async Task IsImageExists_IfNotExists_ReturnsNULL()
        {
            //Arrange
            ImageService imageService = new ImageService();

            string filePath = Path.GetFullPath(@"testfiles\Mountain.png");
            FileStream fileStream = new FileStream(filePath, FileMode.Open);
            var TestImageFile = new MyTestPostedFileBase(fileStream, "images/png", "Mountain.png");
                 
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
