using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using ImageCarousel.Models;

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
            ImageModel imageObject = new ImageModel();
            return View(imageObject);
        } 
    }
}