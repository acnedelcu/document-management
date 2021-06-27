using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using DocumentManagement.Models;
using Microsoft.AspNetCore.Identity;

namespace DocumentManagement.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly UserManager<ApplicationUser> userManager;
        private const string DefaultPassword = "P@rola123";
        public UserController(IWebHostEnvironment webHostEnvironment, UserManager<ApplicationUser> userManager)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Import()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ImportUserAsync()
        {
            List<ApplicationUser> applicationUsers = new List<ApplicationUser>();

            var file = Request.Form.Files["File"];
            string filePath = @Path.Combine(webHostEnvironment.WebRootPath, "userImports");
            string filePathWithFileName = Path.Combine(filePath, file.FileName);

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            using (var fileStream = System.IO.File.Create(@filePathWithFileName))
            {
                file.CopyTo(fileStream);
            }

            //read user info from the excel file
            applicationUsers = Helper.ReadExcelData(@filePathWithFileName);
            if(applicationUsers.Count!=0)
            {
                foreach(var appUser in applicationUsers)
                {
                    var result = await userManager.CreateAsync(appUser, DefaultPassword);
                    if(result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(appUser, "User");
                    }
                }
            }

            System.IO.File.Delete(@filePathWithFileName);
            return View("Import");
        }
    }
}
