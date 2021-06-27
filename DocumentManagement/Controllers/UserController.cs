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
using DocumentManagement.ViewModels;

namespace DocumentManagement.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        #region constants and dependencies
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IApplicationUserRepository applicationUserRepository;
        private readonly IGroupRepository groupRepository;
        private const string DefaultPassword = "P@rola123";
        #endregion
        public UserController(IWebHostEnvironment webHostEnvironment, IApplicationUserRepository applicationUserRepository, IGroupRepository groupRepository, UserManager<ApplicationUser> userManager)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.applicationUserRepository = applicationUserRepository;
            this.userManager = userManager;
            this.groupRepository = groupRepository;
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

        [HttpGet]
        public IActionResult SelectUser(GenerateViewModel generateViewModel)
        {
            
            GenerateViewModel viewModel = new GenerateViewModel
            {
                ApplicationUsers = applicationUserRepository.AllApplicationUsers,
                Groups = groupRepository.AllGroups
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult RedirectToList (GenerateViewModel generateViewModel)
        {
            ApplicationUser selectedUser = applicationUserRepository.GetUserByNames(generateViewModel.FirstName, generateViewModel.LastName).FirstOrDefault();
            string selectedUserUsername = selectedUser.UserName;
            return RedirectToAction("List", "Document", new { Username = selectedUserUsername});
        }
    }
}
