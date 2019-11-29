using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ImageCarousel.Controllers;

namespace ImageCarousel.Models
{
    public class ImageModel
    {
        public static string randompath
        {
            get { return ImageCarousel.Controllers.HomeController.GetRandomImageFullPath(); }
            set { }
        }
    }
}