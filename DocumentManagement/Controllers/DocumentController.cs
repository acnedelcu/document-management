using DocumentManagement.BlobStorage;
using DocumentManagement.Models;
using DocumentManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Controllers
{
    [Authorize]
    public class DocumentController: Controller
    {
        private readonly IConfiguration configuration;
        private readonly IApplicationUserRepository applicationUserRepository;
        private readonly UserManager<ApplicationUser> userManager;
        public DocumentController(IConfiguration configuration, IApplicationUserRepository applicationUserRepository, UserManager<ApplicationUser> userManager)
        {
            this.configuration = configuration;
            this.applicationUserRepository = applicationUserRepository;
            this.userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public async Task<ViewResult> List(string Username)
        {
            FileHandler fileHandler = new FileHandler(configuration);
            ListViewModel listViewModel = new ListViewModel();
            ApplicationUser applicationUser;
            if (String.IsNullOrEmpty(Username))
            {
                applicationUser = applicationUserRepository.GetUserWithId(userManager.GetUserId(this.User));
            }
            else
            {
                applicationUser = applicationUserRepository.GetUserWithUsername(Username);
            }

            List<string> fileNames = new List<string>();

            //we cannot pass directly a List<String> to the Datagrid; we need a wrapper class
            fileNames = await fileHandler.ListFiles(applicationUser);
            foreach(var file in fileNames)
            {
                listViewModel.BlobNames.Add(new DatagridFileWrapper { BlobName = file });
                listViewModel.ApplicationUser = applicationUser;
            }
            return View(listViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, User")]
        public IActionResult Open(ListViewModel listViewModel)
        {
            FileHandler fileHandler = new FileHandler(configuration);

            ApplicationUser applicationUser = applicationUserRepository.GetUserWithUsername(listViewModel.Username);
            string selectedFileName = listViewModel.SelectedFileName;
            string blobSasUrl = fileHandler.GenerateBlobSasUrl(applicationUser, selectedFileName);
            return Redirect(blobSasUrl);
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public ViewResult Claim()
        {
            var viewModel = new ClaimViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public ActionResult Claim(ClaimViewModel viewModel)
        {
            string documentType = viewModel.DocumentType;
            string description = viewModel.Description;
            ApplicationUser applicationUser = applicationUserRepository.GetUserWithUsername(User.Identity.Name);

            //add the request to the FileRequestList
            Helper.FileRequests.Add(new FileRequest { ApplicationUser = applicationUser, DocumentType = documentType, Description = description });

            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult FileRequests()
        {
            var viewModel = new FileRequestViewModel();
            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        public async Task<ViewResult> ListForQr(GenerateViewModel adminViewModel)
        {
            FileHandler fileHandler = new FileHandler(configuration);
            ListViewModel listViewModel = new ListViewModel();
            ApplicationUser applicationUser = applicationUserRepository.GetUserByNames(adminViewModel.FirstName, adminViewModel.LastName).FirstOrDefault();

            List<string> fileNames = new List<string>();

            //we cannot pass directly a List<String> to the Datagrid; we need a wrapper class
            fileNames = await fileHandler.ListFiles(applicationUser);
            listViewModel.ApplicationUser = applicationUser;
            foreach (var file in fileNames)
            {
                listViewModel.BlobNames.Add(new DatagridFileWrapper { BlobName = file });
            }
            return View(listViewModel);
        }
    }
}
