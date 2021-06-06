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
        public ViewResult Send(int id)
        {
            if (id == -1 || id==0)
            {
                var uploadViewModel = new UploadViewModel
                { Groups = this.groupRepository.AllGroups, StudyPrograms = this.studyProgramRepository.AllStudyPrograms,
                ApplicationUsers = this.applicationUserRepository.AllApplicationUsers};
                return View(uploadViewModel);
            }
            else
            {
                var uploadViewModel = new UploadViewModel
                {
                    Groups = this.groupRepository.AllGroups,
                    StudyPrograms = this.studyProgramRepository.AllStudyPrograms,
                    ApplicationUsers = new List<ApplicationUser> { Helper.FileRequests[id].ApplicationUser }  
                };
                Helper.FileRequests.RemoveAt(id);
                return View(uploadViewModel);
            }
        }

        [HttpPost]
        //[Authorize]
        public async Task<ActionResult> Send(UploadViewModel uploadViewModel)
        {
            //retrieving the data submitted
            var file = Request.Form.Files["File"];
            string group = uploadViewModel.Group;
            string firstName = uploadViewModel.FirstName;
            string lastName = uploadViewModel.LastName;

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

            //the lists will be empty on post and we need to repopulate them
            uploadViewModel.ApplicationUsers = this.applicationUserRepository.AllApplicationUsers;
            uploadViewModel.StudyPrograms = this.studyProgramRepository.AllStudyPrograms;
            uploadViewModel.Groups = this.groupRepository.AllGroups;

            return View(uploadViewModel);
        }
    }
}
