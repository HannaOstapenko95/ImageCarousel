using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace ImageCarousel.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Return index (i.e. start page) of the website upon user request to fqdn of the service
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// This method implements basic carousel algorithm of 'ImageCarousel' service
        /// </summary>
        /// <returns></returns>
        public static string GetRandomImageFullPath()
        {
            var page = new Page();
            var fileInfo = new System.IO.FileInfo(page.Server.MapPath("/images/"));                  
            List<System.IO.FileInfo> imagesToShow = fileInfo.Directory.GetFiles().ToList();
            
            Random rand = new Random();            
            int index = rand.Next(1, imagesToShow.Count);
            string selectedFqnOfRandomImage = Convert.ToString(@"\images\" + imagesToShow[index]);

            imagesToShow.RemoveAt(index);

            return selectedFqnOfRandomImage;
        }
    }
}