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
        public UploadController(IGroupRepository groupRepository)
        {
            this.groupRepository = groupRepository;
        }
        public ViewResult Send()
        {
            var uploadViewModel = new UploadViewModel { Groups = this.groupRepository.AllGroups };
            return View(uploadViewModel);
        }
    }
}
