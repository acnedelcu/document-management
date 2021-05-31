using DocumentManagement.BlobStorage;
using DocumentManagement.Models;
using DocumentManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Controllers
{
    public class UploadController : Controller
    {
        private readonly IGroupRepository groupRepository;
        private readonly IStudyProgramRepository studyProgramRepository;
        private readonly IApplicationUserRepository applicationUserRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IConfiguration configuration;
        public UploadController(IGroupRepository groupRepository, IStudyProgramRepository studyProgramRepository, IApplicationUserRepository applicationUserRepository, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            this.groupRepository = groupRepository;
            this.studyProgramRepository = studyProgramRepository;
            this.applicationUserRepository = applicationUserRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.configuration = configuration;

        }

        [HttpGet]
        //[Authorize]
        public ViewResult Send()
        {
            var uploadViewModel = new UploadViewModel
            { Groups = this.groupRepository.AllGroups, StudyPrograms = this.studyProgramRepository.AllStudyPrograms,
            ApplicationUsers = this.applicationUserRepository.AllApplicationUsers};
            return View(uploadViewModel);
        }

        [HttpPost]
        //[Authorize]
        public async Task<ActionResult> SendFiles()
        {
            //retrieving the data submitted
            string group = Request.Form["Group"].ToString();
            string firstName = Request.Form["FirstName"].ToString();
            string lastName = Request.Form["LastName"].ToString();
            var file = Request.Form.Files["File"];
            if(!String.IsNullOrWhiteSpace(group) && !String.IsNullOrWhiteSpace(firstName) && !String.IsNullOrWhiteSpace(lastName))
            {
                ApplicationUser applicationUser = applicationUserRepository.GetUserByNames(firstName, lastName).FirstOrDefault();

                var filePath = Path.Combine(webHostEnvironment.WebRootPath, "uploads");

                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);

                using (var fileStream = System.IO.File.Create(Path.Combine(filePath, file.FileName)))
                {
                    file.CopyTo(fileStream);
                }
                FileHandler fileHandler = new FileHandler(configuration);
                await fileHandler.UploadFile(Path.Combine(filePath, file.FileName).ToString(), applicationUser);
                System.IO.File.Delete(Path.Combine(filePath, file.FileName));

                return new EmptyResult();









            }

            return new EmptyResult();
        }
    }
}
