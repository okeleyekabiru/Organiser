using Newtonsoft.Json;
using System;
using System.IO;

namespace Organiser
{
    public static class Utilities 
    { 
        public static string SerializeSettings<T>(this T data)
        {
            return JsonConvert.SerializeObject(data);
        }
        public static T DeserializeSettings<T>(this string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }


        public static void EnumerateAndMoveCategorize()
        {
            var downloadPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads";
            foreach (var item in Directory.GetFiles(downloadPath))
            {

                if (item.EndsWith(".aif")
                     || item.EndsWith(".cda")
                     || item.EndsWith(".mid")
                     || item.EndsWith(".midi")
                     || item.EndsWith(".mp3")
                     || item.EndsWith(".mpa")
                     || item.EndsWith(".ogg")
                     || item.EndsWith(".wav")
                     || item.EndsWith(".wma")
                     || item.EndsWith(".wpl"))

                    Categorize(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic), item);
                else if (item.EndsWith(".ai")
                     || item.EndsWith(".tiff")
                     || item.EndsWith(".tif")
                     || item.EndsWith(".svg")
                     || item.EndsWith(".psd")
                     || item.EndsWith(".ps")
                     || item.EndsWith(".png")
                     || item.EndsWith(".jpg")
                     || item.EndsWith(".jpeg")
                     || item.EndsWith(".ico")
                     || item.EndsWith(".gif ")
                     || item.EndsWith(".bmp"))
                    Categorize(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), item);
                else if (item.EndsWith(".xlsx")
                   || item.EndsWith(".xlsm")
                   || item.EndsWith(".xls")
                   || item.EndsWith(".ods")
                   || item.EndsWith(".wpd")
                   || item.EndsWith(".txt")
                   || item.EndsWith(".tex")
                   || item.EndsWith(".rtf")
                   || item.EndsWith(".pdf")
                   || item.EndsWith(".odt")
                   || item.EndsWith(".docx")
                   || item.EndsWith(".doc"))
                    Categorize(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), item);

                else if (item.EndsWith(".wmv")
                  || item.EndsWith(".vob")
                  || item.EndsWith(".swf")
                  || item.EndsWith(".rm")
                  || item.EndsWith(".mpeg")
                  || item.EndsWith(".mpg")
                  || item.EndsWith(".mp4")
                  || item.EndsWith(".mov")
                  || item.EndsWith(".mkv")
                  || item.EndsWith(".m4v")
                  || item.EndsWith(".h264")
                  || item.EndsWith(".flv")
                  || item.EndsWith(".avi")
                  || item.EndsWith(".3gp")
                  || item.EndsWith(".3g2"))
                    Categorize(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos), item);



            };
        }

        static void Categorize(string destination, string filePath)
        {



            if (File.Exists(filePath))
            {
                var file = destination.Split(@"\");
                var newFIlePath = filePath.Replace("Downloads", file[file.Length - 1]);
                File.Move(filePath, newFIlePath, true);

            }
        }

        
    }
}
