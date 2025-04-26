using ECommerce.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Repositories.Services
{
    public class ImageManagementService : IImageManagementService
    {
        private readonly IFileProvider fileProvider;
        public ImageManagementService(IFileProvider fileProvider)
        {
            this.fileProvider = fileProvider;
        }

        public async Task<List<string>> AddImageAsync(IFormFileCollection files, string src)
        {
            var SaveImageSrc = new List<string>();
            var ImageDirectory = Path.Combine("wwwroot", "Images", src);
            if (Directory.Exists(ImageDirectory) is not true)
            {
                Directory.CreateDirectory(ImageDirectory);
            }

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var imageName = file.FileName;
                    var imageSrc = $"/Images/{src}/{imageName}";
                    var root = Path.Combine(ImageDirectory, imageName);

                    using(FileStream stream = new FileStream(root, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    SaveImageSrc.Add(imageSrc);
                }
            }
            return SaveImageSrc;
        }

        public void DeleteImage(string src)
        {
           var info = fileProvider.GetFileInfo(src);
            var root = info.PhysicalPath;
            File.Delete(root);
        }
    }
}
