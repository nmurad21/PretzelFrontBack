using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PretzelFrontToBack.Helpers
{
    public static class Helper
    {
        public static void DeleteFile(IWebHostEnvironment env,string folder,string images, string filename)
        {
            string path = env.WebRootPath;
            string resultPath = Path.Combine(path, folder,images, filename);
            if (System.IO.File.Exists(resultPath))
            {
                System.IO.File.Delete(resultPath);
            }
        }
    }
}
