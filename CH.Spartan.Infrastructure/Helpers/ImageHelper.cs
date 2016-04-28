using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace CH.Spartan.Infrastructure
{
   public static class ImageHelper
    {
       public static string SaveBase64ToImage(string base64String)
       {
           var root = @HostingEnvironment.ApplicationPhysicalPath;
           var filePath = $"{root}/UploadFiles/Avatar/{DateTime.Now.ToString("yyyyMMdd")}";
           if (!Directory.Exists(filePath))
           {
               Directory.CreateDirectory(filePath);
           }
           var array = base64String.Split(',');
           //data:image/png;base64
           var ext = array[0].Split('/')[1].Split(';')[0];
           var text = array[1];
           var fileName = filePath + "/" + Guid.NewGuid() + "." + ext;
           var bytes = Convert.FromBase64String(text);
           using (var imageFile = new FileStream(fileName, FileMode.Create))
           {
               imageFile.Write(bytes, 0, bytes.Length);
               imageFile.Flush();
           }
           return fileName.Replace(root,"");
       }
    }
}
