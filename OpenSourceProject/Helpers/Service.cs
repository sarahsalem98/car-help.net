using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using OpenSourceProject.Models;
using System.Drawing;

namespace OpenSourceProject.Helpers
{
    public class Service
    {
        

       public string AddFilesToOrder(IWebHostEnvironment webHost,IFormFile file,Order order)
        {
            string wwwRootPath = webHost.WebRootPath;

            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString();
                var extention = Path.GetExtension(file.FileName);
                var uploads = Path.Combine(wwwRootPath, @"images\clients\orders");
                if (order.Image != null)
                {
                    var oldImagePath = Path.Combine(wwwRootPath, order.Image.Trim('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }

                }
                using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extention), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                return @"images\clients\orders\" + fileName + extention;
            }
            else
            {
                return null;
            }

        }

        public List<string> AddFilesToProduct(IWebHostEnvironment webHost,List<IFormFile>files,Product product)
        {
            string wwwRootPath = webHost.WebRootPath;
            List<string> productFiles = new List<string>(); 
            foreach(var file in files) { 
              if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var extention = Path.GetExtension(file.FileName);
                    var uploads = Path.Combine(wwwRootPath, @"images\providers\products");
                    if (product.Image != null)
                    {
                        foreach(var image in product.Image)
                        {

                        var oldImagePath = Path.Combine(wwwRootPath, image.Trim('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                        }

                    }
                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extention), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productFiles.Add(@"images\providers\products\" + fileName + extention);
                }
                else
                {
                    return null;
                }
            
            }

                    return productFiles;

        }


        public string AddFilesToProvider(IWebHostEnvironment webHost, IFormFile file, string fileType, Provider user)
        {


            string wwwRootPath = webHost.WebRootPath;

            if (fileType == "workShopPhoto")
            {

                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var extention = Path.GetExtension(file.FileName);
                    var uploads = Path.Combine(wwwRootPath, @"images\providers\workshops");
                    if (user.WorkShopPhotoUrl != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, user.WorkShopPhotoUrl.Trim('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }

                    }
                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extention), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    return @"images\providers\workshops\" + fileName + extention;
                }
                else
                {
                    return null;
                }
              
            }
            else if (fileType == "registerationFile")
            {
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var extention = Path.GetExtension(file.FileName);
                    var uploads = Path.Combine(wwwRootPath, @"images\providers\registerationfiles");
                    if (user.RegisterationFile != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, user.RegisterationFile.Trim('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }

                    }
                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extention), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    return @"images\providers\registerationfiles\" + fileName + extention;
                }
                else
                {
                    return null;
                }
            }
            return null;


        }



    }
}
