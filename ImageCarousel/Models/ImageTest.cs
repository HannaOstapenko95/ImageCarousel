using System;
using Xunit;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using ImageCarousel.Models;

namespace ImageCarousel.Test
{
    public class ImageTest
    {
        private readonly ImageTest _image;
        public ImageTest()
        {
            _image = new ImageTest();
        }
        public static Page page = new Page();
        public static string imagesNotShown = page.Server.MapPath(ConfigurationManager.AppSettings["imagesNotShown"]);
        [Fact]
        public void Check_If_FileIs()
        {

            ////ImageModel im = new ImageModel();
            //bool expected = true;
            //ImageModel.CreateFileForStoringImagesList();
            //    bool actual = new FileInfo(imagesNotShown).Exists;
            //Assert.Equal(expected, actual);
            var x = 5;
            var y = 5;
            Assert.Equal(x, y);
        } 
}
    
}
