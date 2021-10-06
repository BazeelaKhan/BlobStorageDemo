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
        /// Checks Whether the image already exixts in Blob container
        /// </summary>
        /// <param name="imageToUpload"></param>
        /// <returns> 
        ///           URl- if image exists
        ///           null - if not exists
        /// </returns>
        public async Task<string> IsImageExists(HttpPostedFileBase imageToUpload)
        {
            String imageFullPath = null;
            if (imageToUpload == null || imageToUpload.ContentLength == 0)
            {
                return null;
            }

            try
            {
                BlobServiceClient blobServiceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=imageresizerastorage;AccountKey=lWK00MRrlv/flVxqSUg0PeVX5ZapjVDcYHyKPWdUHa8A9bY+TFulOMvhh+aR821z7OzvrMrRd66AebIzIRVSXg==;EndpointSuffix=core.windows.net"); // Connection String
                BlobContainerClient container = blobServiceClient.GetBlobContainerClient("normal-size");// Container1 name 
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
                Console.WriteLine("Exception",ex);
            }
            return null;
        }
        /// <summary>
        /// Method to check the correct extension of file.
        /// </summary>
        /// <param name="fileExtension"></param>
        /// <returns>
        ///          true- if extension correct
        ///          false- if extension is incorrect
        /// </returns>
        public bool CheckExtension(String fileExtension)
        {
            if (fileExtension == "jpeg" || fileExtension == "png" || fileExtension == "jpg")
                return true;
            else
                return false;
        }
        /// <summary>
        /// This method will create the container if not exists and then upload the image into the container
        /// after uploading the image it generates the string of ImagePath 
        /// </summary>
        /// <param name="imageToUpload"></param>
        /// <returns>
        ///           url- image url after uploading the image
        /// </returns>
        public async Task<string> UploadImageAsync(HttpPostedFileBase imageToUpload)
        {
            string imageFullPath = null;
            if (imageToUpload == null || imageToUpload.ContentLength == 0)
            {
                return null;
            }
            try
            {

                BlobServiceClient blobServiceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=imageresizerastorage;AccountKey=lWK00MRrlv/flVxqSUg0PeVX5ZapjVDcYHyKPWdUHa8A9bY+TFulOMvhh+aR821z7OzvrMrRd66AebIzIRVSXg==;EndpointSuffix=core.windows.net");// Connection String of Storage Account
                BlobContainerClient container = blobServiceClient.GetBlobContainerClient("normal-size"); // Container 1 Name
                await container.CreateIfNotExistsAsync();
                
                    string imageName = imageToUpload.FileName;
                    string FileExtension = imageName.Substring(imageName.LastIndexOf('.') + 1).ToLower();

                    if(CheckExtension(FileExtension))
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
                Console.WriteLine("Exception", ex);
            }
            return imageFullPath;
        }

        public string Demo (HttpPostedFileBase image)
        {
            String url = null;
            string filename = image.FileName;
            url = "http://normal-size/" + filename;
            return url;
        }
        /// <summary>
        /// Method to download the thumbnail image.
        /// </summary>
        /// <param name="imageToDownload"></param>
        /// <returns> url of the thubmnail image </returns>
        public async Task<string> DownloadThumbnail(HttpPostedFileBase imageToDownload)
        {
            String imageFullPath = null;
            try
            {
                BlobServiceClient blobServiceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=imageresizerastorage;AccountKey=lWK00MRrlv/flVxqSUg0PeVX5ZapjVDcYHyKPWdUHa8A9bY+TFulOMvhh+aR821z7OzvrMrRd66AebIzIRVSXg==;EndpointSuffix=core.windows.net");// Connection String of Storage account
                BlobContainerClient container = blobServiceClient.GetBlobContainerClient("reduced-size");// container2 name
                await container.CreateIfNotExistsAsync();

                string imageName = imageToDownload.FileName;
                BlobClient blob = container.GetBlobClient(imageName);
                //var str = blobServiceClient.GetBlobContainerClient("normal-size").GetBlobClient(imageName);
                imageFullPath = blob.Uri.ToString();
                return imageFullPath;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception", ex);
            }
            return null;
        }
    }
}