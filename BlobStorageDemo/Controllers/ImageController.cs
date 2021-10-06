using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BlobStorageDemo.Controllers
{
    public class ImageController : Controller
    {
        ImageService imageService = new ImageService();


        // GET: Image  
        public ActionResult Upload()
        {
            return View();
        }
        /// <summary>
        /// Upload Method is used to check the image already exists or not if not then 
        /// upload the image to normal-size container
        /// and take the image url from both normal-size and reduced-size container
        /// and pass it to the view 
        /// </summary>
        /// <param name="photo"></param>
        /// <returns>view</returns>
        [HttpPost]
        public async Task<ActionResult> Upload(HttpPostedFileBase photo)
        {
            var check = await imageService.IsImageExists(photo);
            if (check != null)
            {
                TempData["Exist"] = check.ToString();
                var thumbUrl = await imageService.DownloadThumbnail(photo);
                TempData["Thumbnail"] = thumbUrl.ToString();
                return RedirectToAction("AlreadyExist");

            }
            else
            {
                var imageUrl = await imageService.UploadImageAsync(photo);
                if (imageUrl == null)
                {
                    TempData["Errormsg"] = "Please upload image or file of extension .jpg, .jpeg or .png";
                    return View("Upload");
                }
                else
                {
                    TempData["LatestImage"] = imageUrl.ToString();
                    Task.WaitAll(Task.Delay(5000));
                    var thumbUrl = await imageService.DownloadThumbnail(photo);
                    TempData["Thumbnail"] = thumbUrl.ToString();
                    return RedirectToAction("LatestImage");
                }
            }
        }
        /// <summary>
        /// This method pass the URLs through viewbag
        /// to the view to display the original and thumbnail image
        /// </summary>
        /// <returns>view</returns>
        public ActionResult LatestImage()
        {

            if (TempData["LatestImage"] != null && TempData["Thumbnail"] != null)
            {
                ViewBag.LatestImage = Convert.ToString(TempData["LatestImage"]);
                ViewBag.Thumbnail = Convert.ToString(TempData["Thumbnail"]);
               
            }

            return View();
        }
        /// <summary>
        /// This method will be called if the image already exists in container 
        /// it pass the URL to the View to display the original and thumbnail image
        /// </summary>
        /// <returns>view</returns>
        public ActionResult AlreadyExist()
        {
            if (TempData["Exist"] != null && TempData["Thumbnail"]!=null)
            {
                ViewBag.Exist = Convert.ToString(TempData["Exist"]);
                ViewBag.Thumbnail = Convert.ToString(TempData["Thumbnail"]);
            }

            return View();
        }
    }
}