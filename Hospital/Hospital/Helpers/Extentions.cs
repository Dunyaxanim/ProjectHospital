using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Helpers
{
    public static class Extentions
    {
        public static bool IsImage(this IFormFile file)
        {
            return file.ContentType.Contains("image/");
        }
        public static bool IsElder400kb(this IFormFile file)
        {
            return file.Length/1024>400;
        }
        public static async Task<string> SaveFileAsync(this IFormFile file,string folder)
        {
            string filename = Guid.NewGuid().ToString() + file.FileName;
            string path =  Path.Combine(folder, filename);
            using (FileStream fileStream = new (path, FileMode.Create))
            {
               await file.CopyToAsync(fileStream);
            }
            return filename;
        }
    }
}
