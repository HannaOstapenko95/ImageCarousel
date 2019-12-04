using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using ImageCarousel.Controllers;

namespace ImageCarousel
{
    public class Image
    {
        /// <summary>
        /// This method writes the list of image names from a folder into the txt file if the file is empty and just created
        /// </summary>
        /// <returns></returns>
        public static void WriteImagesNamesInFile()
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (new FileInfo(HomeController.imagesNotShown).Length == 0)
            { 
                for (int imageIndex = 0; imageIndex < HomeController.imagesToShow.Count; imageIndex++)
                {
                    using (StreamWriter streamWriter = new StreamWriter(HomeController.imagesNotShown, false, System.Text.Encoding.Default))
                    {
                        string imageName = Path.GetFileName(HomeController.folderWithImages + HomeController.imagesToShow[imageIndex]);
                        streamWriter.WriteLine(stringBuilder.AppendLine(imageName));
                    }
                }
            }
        }

        /// <summary>
        /// This method generates random index for further image selection (from 0 to the number of image name in file - 1)
        /// </summary>
        /// <returns></returns>
        public static int GetRandomIndex()
        {
            int index;
            if (new FileInfo(HomeController.imagesNotShown).Length == 0)
            {
                Random rand = new Random();
                index = rand.Next(0, HomeController.imagesToShow.Count);
            }
            else
            {
                Random rand = new Random();
                int numberOfLinesInFile = File.ReadAllLines(HomeController.imagesNotShown).Length-1;
                index = rand.Next(0, numberOfLinesInFile-1);
            }
            return index;
        }

        /// <summary>
        /// This method retrieves image name written in txt file via reading line in file via selected random index
        /// </summary>
        /// <returns></returns>
        public static string GetRandomImageNameFromFile(int randomIndex)
        {
            string randomImageName;
            using (StreamReader fileWithImagesList = new StreamReader(HomeController.imagesNotShown))
            {
                randomImageName = ReadSpecificLine(HomeController.imagesNotShown, randomIndex + 1);
            }
            return randomImageName;
        }

        /// <summary>
        /// This method retrieves the image name (value of line in txt file) via specifying line number (selected random index)
        /// </summary>
        /// <returns></returns>
        static string ReadSpecificLine(string filePath, int lineNumber)
        {
            string content = null;
            try
            {
                using (StreamReader file = new StreamReader(filePath))
                {
                    for (int i = 1; i < lineNumber; i++)
                    {
                        file.ReadLine();

                        if (file.EndOfStream)
                        {
                            break;
                        }
                    }
                    content = file.ReadLine();
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("There was an error reading the file: ");
                Console.WriteLine(e.Message);
            }
            return content;
        }

        /// <summary>
        /// This method deletes txt file for storing images' names
        /// </summary>
        /// <returns></returns>
        public static void DeleteFileWithImageNames()
        {
            File.Delete(HomeController.imagesNotShown);
        }

        /// <summary>
        /// This method creates txt file for storing images' names 
        /// </summary>
        /// <returns></returns>
        public static void CreateFileForStoringImagesList()
        {
            var fileForStoringImagesList = File.Create(HomeController.imagesNotShown);
            fileForStoringImagesList.Close();

        }
    }
}