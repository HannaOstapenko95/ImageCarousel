using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using ImageCarousel;

namespace ImageCarousel.Controllers
{
    public class HomeController : Controller
    {
        static Page page = new Page();
        public static string folderWithImages = ConfigurationManager.AppSettings["folderWithImages"];
        static System.IO.FileInfo fileInfo = new System.IO.FileInfo(page.Server.MapPath(folderWithImages));
        public static List<System.IO.FileInfo> imagesToShow = fileInfo.Directory.GetFiles().ToList();
        public static string fileWithImagesListPath = ConfigurationManager.AppSettings["filePath"];
        

        /// <summary>
        /// Return index (i.e. start page) of the website upon user request to fqdn of the service
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// This method implements carousel algorithm of 'ImageCarousel' service:
        /// - creates txt file for storing images' names list and fill it with the images' names from a folder
        /// - generates random index and retrieves image name from a txt filr via this index
        /// - generates Image path according to selected image
        /// - removes showed image from a list and updates txt file 
        /// - deletes txt file if all images are shown
        /// </summary>
        /// <returns></returns>
        public static string GetRandomImagePath()
        {
            if (!new FileInfo(fileWithImagesListPath).Exists)
            {
                Image.CreateFileForStoringImagesList();
            }

            Image.WriteImagesNamesInFile();
            int randomIndex = Image.GetRandomIndex();
            string selectedImagePath = Convert.ToString(folderWithImages + Image.GetRandomImageNameFromFile(randomIndex));

            List<string> updatedImagesListInFile = RemoveShowedImageFromList(randomIndex);
            UpdateFileWithNewImageList(updatedImagesListInFile);
            DeleteFileIfEmpty(updatedImagesListInFile);

            return selectedImagePath;
        }

        /// <summary>
        /// This method checks the current images names which are written in file, removes from List already showed image,
        /// checks the updated List with image names
        /// </summary>
        /// <returns></returns>
        public static List<string> RemoveShowedImageFromList(int randomIndex)
        {
            //Check the initial Images' names in file
            var imagesNamesFromFile = new List<string>(System.IO.File.ReadAllLines(fileWithImagesListPath));
            for (int rowIndexInFile = 0; rowIndexInFile < imagesNamesFromFile.Count - 1; rowIndexInFile++)
            {
                string imageNameInFile = imagesNamesFromFile[rowIndexInFile];
            }

            //Remove already showed image from file Image.GetRandomIndex()
            imagesNamesFromFile.RemoveAt(randomIndex );

            //Check the Images' names in file after removing the showed one
            for (int rowIndexInFile = 0; rowIndexInFile < imagesNamesFromFile.Count - 1; rowIndexInFile++)
            {
                string imageNameInFileAfterRemoval = imagesNamesFromFile[rowIndexInFile];
            }
            return imagesNamesFromFile;
        }

        /// <summary>
        /// This method updates txt file - writes in file the updated list of image names after removal showed one
        /// </summary>
        /// <returns></returns>
        public static void UpdateFileWithNewImageList(List<string> imagesNamesFromFile)
        {
            StringBuilder stringBuilderUpdateImageNames = new StringBuilder();
            for (int imageIndex = 0; imageIndex < imagesNamesFromFile.Count - 1; imageIndex++)
            {
                using (StreamWriter swUpdateImageNamesInFile = new StreamWriter(fileWithImagesListPath, false, System.Text.Encoding.Default))
                {
                    string imageNameAfterUpdate = Path.GetFileName(folderWithImages + imagesNamesFromFile[imageIndex]);
                    swUpdateImageNamesInFile.WriteLine(stringBuilderUpdateImageNames.AppendLine(imageNameAfterUpdate));
                }
            }
        }

        /// <summary>
        /// This method deletes txt file if all images from the file - have been already shown
        /// </summary>
        /// <returns></returns>
        public static void DeleteFileIfEmpty(List<string> imagesNamesFromFile)
        {
            //Delete File if nothing to show left in file
            if (imagesNamesFromFile.Count == 1)
            {
                Image.DeleteFileWithImageNames();
            }   
        }
    }
}