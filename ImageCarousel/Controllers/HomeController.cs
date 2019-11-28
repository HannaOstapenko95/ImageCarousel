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
        public ActionResult Index()
        {
            Page p = new Page();
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(p.Server.MapPath("/images/"));
            System.IO.DirectoryInfo dirFileUpload = fileInfo.Directory;
            List<System.IO.FileInfo> possible = dirFileUpload.GetFiles().ToList();
            Random rand = new Random();
            string randompath = "";
            int index = rand.Next(1, possible.Count);
            randompath = Convert.ToString(@"\images\" + possible[index]);
            ViewBag.Message = randompath;
            possible.RemoveAt(index);
            return View();
        }
    }
}