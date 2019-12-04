using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace ImageCarousel.Models
{
    public class ImageModel
    {

        public static string selectedImagePath { get; set; }
        static Page page = new Page();
        public static string folderWithImages = ConfigurationManager.AppSettings["folderWithImages"];
        static System.IO.FileInfo fileInfo = new System.IO.FileInfo(page.Server.MapPath(folderWithImages));
        public static List<System.IO.FileInfo> imagesToShow = fileInfo.Directory.GetFiles().ToList();
        public static string imagesNotShown = page.Server.MapPath(ConfigurationManager.AppSettings["imagesNotShown"]);

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
            if (!new FileInfo(imagesNotShown).Exists)
            {
                ImageModel.CreateFileForStoringImagesList();
            }

            ImageModel.WriteImagesNamesInFile();
            int randomIndex = ImageModel.GetRandomIndex();
            string selectedImagePath = Convert.ToString(folderWithImages + ImageModel.GetRandomImageNameFromFile(randomIndex));

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
            var imagesNamesFromFile = new List<string>(System.IO.File.ReadAllLines(imagesNotShown));
            for (int rowIndexInFile = 0; rowIndexInFile < imagesNamesFromFile.Count - 1; rowIndexInFile++)
            {
                string imageNameInFile = imagesNamesFromFile[rowIndexInFile];
            }

            //Remove already showed image from file Image.GetRandomIndex()
            imagesNamesFromFile.RemoveAt(randomIndex);

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
                using (StreamWriter swUpdateImageNamesInFile = new StreamWriter(imagesNotShown, false, System.Text.Encoding.Default))
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
                ImageModel.DeleteFileWithImageNames();
            }
        }

        /// <summary>
        /// This method writes the list of image names from a folder into the txt file if the file is empty and just created
        /// </summary>
        /// <returns></returns>
        public static void WriteImagesNamesInFile()
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (new FileInfo(imagesNotShown).Length == 0)
            {
                for (int imageIndex = 0; imageIndex < imagesToShow.Count; imageIndex++)
                {
                    using (StreamWriter streamWriter = new StreamWriter(imagesNotShown, false, System.Text.Encoding.Default))
                    {
                        string imageName = Path.GetFileName(folderWithImages + imagesToShow[imageIndex]);
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
            if (new FileInfo(imagesNotShown).Length == 0)
            {
                Random rand = new Random();
                index = rand.Next(0, imagesToShow.Count);
            }
            else
            {
                Random rand = new Random();
                int numberOfLinesInFile = File.ReadAllLines(imagesNotShown).Length - 1;
                index = rand.Next(0, numberOfLinesInFile - 1);
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
            using (StreamReader fileWithImagesList = new StreamReader(imagesNotShown))
            {
                randomImageName = ReadSpecificLine(imagesNotShown, randomIndex + 1);
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
            File.Delete(imagesNotShown);
        }

        /// <summary>
        /// This method creates txt file for storing images' names 
        /// </summary>
        /// <returns></returns>
        public static void CreateFileForStoringImagesList()
        {
            var fileForStoringImagesList = File.Create(imagesNotShown);
            fileForStoringImagesList.Close();

        }
    }
}