using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Threading.Tasks;
using System.Web;


namespace BlobStorageDemo
{
    public class ImageService
    {
        /// <summary>
        /// This method will create the container if not exists and then upload the image into the container
        /// after uploading the image it generates the string of ImagePath 
        /// </summary>
        /// <param name="imageToUpload"></param>
        /// <returns></returns>
        public async Task<string> UploadImageAsync(HttpPostedFileBase imageToUpload)
        {
            string imageFullPath = null;
            if (imageToUpload == null || imageToUpload.ContentLength == 0)
            {
                return null;
            }
            try
            {

                BlobServiceClient blobServiceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=imageresizerastorage;AccountKey=lWK00MRrlv/flVxqSUg0PeVX5ZapjVDcYHyKPWdUHa8A9bY+TFulOMvhh+aR821z7OzvrMrRd66AebIzIRVSXg==;EndpointSuffix=core.windows.net");
                BlobContainerClient container = blobServiceClient.GetBlobContainerClient("normal-size");
                await container.CreateIfNotExistsAsync();

                string imageName = imageToUpload.FileName;
                string FileExtension = imageName.Substring(imageName.LastIndexOf('.') + 1).ToLower();

                if (FileExtension == "jpeg" || FileExtension == "png" || FileExtension == "jpg")
                {
                    BlobClient blob = container.GetBlobClient(imageName);

                    await blob.UploadAsync(imageToUpload.InputStream,
                    new BlobHttpHeaders()
                    {
                        ContentType = imageToUpload.ContentType
                    });
                    imageFullPath = blob.Uri.ToString();
                }
            }
            catch (Exception ex)
            {

            }
            return imageFullPath;
        }

        /// <summary>
        /// Checks Whether the image already exixts in Blob container
        /// </summary>
        /// <param name="imageToUpload"></param>
        /// <returns></returns>
        public async Task<string> IsImageExists(HttpPostedFileBase imageToUpload)
        {
            String imageFullPath = null;
            if (imageToUpload == null || imageToUpload.ContentLength == 0)
            {
                return null;
            }
            try
            {
                BlobServiceClient blobServiceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=imageresizerastorage;AccountKey=lWK00MRrlv/flVxqSUg0PeVX5ZapjVDcYHyKPWdUHa8A9bY+TFulOMvhh+aR821z7OzvrMrRd66AebIzIRVSXg==;EndpointSuffix=core.windows.net");
                BlobContainerClient container = blobServiceClient.GetBlobContainerClient("normal-size");
                await container.CreateIfNotExistsAsync();

                string imageName = imageToUpload.FileName;
                BlobClient blob = container.GetBlobClient(imageName);
                //var str = blobServiceClient.GetBlobContainerClient("normal-size").GetBlobClient(imageName);

                if (blob.Exists())
                {
                    imageFullPath = blob.Uri.ToString();
                    return imageFullPath;
                }

            }
            catch (Exception ex)
            {

            }
            return null;
        }
    }
}