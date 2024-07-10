using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace WebBH.Extentions
{
    public static class Utilities
    {


        public static string GetRandomKey(int lenght = 5)
        {
            string pattern = "@123456789zxcvbnmasdfghjklqwertyuiop[]{}:~!@#$%^^&*()+";
            Random rd = new Random();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < lenght; i++)
            {
                sb.Append(pattern[rd.Next(0, pattern.Length)]);
            }

            return sb.ToString();
        }

      


        public static string ToTitleCase(string str)
        {
            string result = str;
            if (!string.IsNullOrEmpty(str))
            {
                var words = str.Split(' ');
                for (int index = 0; index < words.Length; index++)
                {
                    var s = words[index];
                    if (s.Length > 0)
                    {
                        words[index] = s[0].ToString().ToUpper() + s.Substring(1);
                    }

                }
                result = string.Join(" ", words);
            }
            return result;
        }





        public static string SEOUrl(this string url)
        {
            url.ToLower().Trim();


            url = Regex.Replace(url, @"[áàạảãâấầậẩẫăắằặẳẵ]", "a");
            url = Regex.Replace(url, @"[éèẹẻẽêếềệểễ]", "e");
            url = Regex.Replace(url, @"[óòọỏõôốồộổỗơớờợởỡ]", "ο");
            url = Regex.Replace(url, @"[úùụủũưứừựửữ]", "u");
            url = Regex.Replace(url, @"[íìịỉĩ]", "i");
            url = Regex.Replace(url, @"[ýỳỵỷỹ]", "y");
            url = Regex.Replace(url, @"[đ]", "d");



            url = Regex.Replace(url.Trim(), @"[^0-9a-z-\s]", "").Trim();

            url = Regex.Replace(url.Trim(), @"\s+", "-");

            url = Regex.Replace(url, @"\s", "-");
            while (true)
            {
                if (url.IndexOf("--") != -1)
                {
                    url = url.Replace("--", "-");

                }
                else
                {
                    break;
                }


            }
            return url;



        }



        public static async Task<string> UploadFile(Microsoft.AspNetCore.Http.IFormFile file, string sDirectory, string sFileName)
        {
            try
            {
                if (sFileName == null) sFileName = file.FileName;
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", sDirectory);
                CreateIfMissing(path);
                string pathFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", sDirectory, sFileName);
                var supportedTypes = new[] { "jpg", "jpeg", "png", "gif" };
                var fileTxt = System.IO.Path.GetExtension(file.FileName).Substring(1);

                if (!supportedTypes.Contains(fileTxt.ToLower()))
                {
                    return null;
                }
                else
                {
                    using (var stream = new FileStream(pathFile, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    return sFileName;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        private static void CreateIfMissing(string path)
        {
            bool folderExists = Directory.Exists(path);
            if (!folderExists)
            {
                Directory.CreateDirectory(path);
            }
        }

    }
}
