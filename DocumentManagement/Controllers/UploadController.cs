using DocumentManagement.Models;
using DocumentManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Controllers
{
    public class UploadController : Controller
    {
        private readonly IGroupRepository groupRepository;
        private readonly IStudyProgramRepository studyProgramRepository;
        private readonly IApplicationUserRepository applicationUserRepository;
        public UploadController(IGroupRepository groupRepository, IStudyProgramRepository studyProgramRepository, IApplicationUserRepository applicationUserRepository)
        {
            this.groupRepository = groupRepository;
            this.studyProgramRepository = studyProgramRepository;
            this.applicationUserRepository = applicationUserRepository;
        }
        public ViewResult Send()
        {
            var uploadViewModel = new UploadViewModel
            { Groups = this.groupRepository.AllGroups, StudyPrograms = this.studyProgramRepository.AllStudyPrograms,
            ApplicationUsers = this.applicationUserRepository.AllApplicationUsers};
            return View(uploadViewModel);
        }
    }
}
