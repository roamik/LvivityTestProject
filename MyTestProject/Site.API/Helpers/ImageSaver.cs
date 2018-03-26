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

    public string SaveImage(byte[] image, IHostingEnvironment env)
    {
      var modelImage = image;

      var imageName = Guid.NewGuid();

      var webRoot = env.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "assets\\");

      var filePath = webRoot + Path.GetFileName(imageName.ToString());

      File.WriteAllBytes(filePath, modelImage);

      return filePath;
    }
  }
}
