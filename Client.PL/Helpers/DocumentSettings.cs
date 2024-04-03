using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;


namespace Client.PL.Helpers
{
    public static class DocumentSettings
    {
        public static async Task<string> UploadFile(IFormFile file, string folderName)

        {
            //1. Get Located Folder Path
            // string folderPath = $"C:\\Users\\Khalefa\\Desktop\\Client.MVC.Solution\\Client.PL\\wwwroot\\files\\Images\\{folderName}";
            // string folderPath =$"{Directory.GetCurrentDirectory()}\\wwwroot\\files{folderName}";

            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName);
            if (Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            //2.Get File Name and Make It Unique
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            //3.file Path
            string filePath = Path.Combine(folderPath, fileName);

            //4.Save File as Streams [Data per Time]
             using var fileStream = new FileStream(filePath, FileMode.Create); 

          await  file.CopyToAsync(fileStream);

            return fileName;
        }

        public static void DeleteFile(string fileName , string folderName)
        {

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
