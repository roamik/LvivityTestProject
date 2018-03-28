using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Site.API.Helpers
{
  public class ImageSaver
  {

    public string SaveImage(IFormFile image, IHostingEnvironment env)
    {
      var modelImage = image;

      var ms = new MemoryStream();

      modelImage.CopyTo(ms);
      var fileBytes = ms.ToArray();

      var webRoot = env.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\");

      var filePath = webRoot + Path.GetFileName(modelImage.FileName);
      var returnFilePath = "http://localhost:55172/images/" + modelImage.FileName;

      File.WriteAllBytes(filePath, fileBytes); //creating file from bytes

      return returnFilePath;
    }
  }
}
