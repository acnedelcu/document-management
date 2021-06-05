﻿using DocumentManagement.BlobStorage;
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
        public async Task<ViewResult> List()
        {
            FileHandler fileHandler = new FileHandler(configuration);
            ListViewModel listViewModel = new ListViewModel();
            ApplicationUser applicationUser = applicationUserRepository.GetUserWithId(userManager.GetUserId(this.User));

            List<string> fileNames = new List<string>();

            //we cannot pass directly a List<String> to the Datagrid; we need a wrapper class
            fileNames = await fileHandler.ListFiles(applicationUser);
            foreach(var file in fileNames)
            {
                listViewModel.BlobNames.Add(new DatagridFileWrapper { BlobName = file });
            }
            return View(listViewModel);
        }

        [HttpPost]
        public JsonResult Open(string selectedFileName)
        {
            FileHandler fileHandler = new FileHandler(configuration);

            ApplicationUser applicationUser = applicationUserRepository.GetUserWithId(userManager.GetUserId(this.User));
            string blobSasUrl = fileHandler.GenerateBlobSasUrl(applicationUser, selectedFileName);
            return Json(blobSasUrl);
        }

        [HttpGet]
        public ViewResult Claim()
        {
            var viewModel = new ClaimViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Claim(ClaimViewModel viewModel)
        {
            string documentType = viewModel.DocumentType;
            string description = viewModel.Description;
            ApplicationUser applicationUser = applicationUserRepository.GetUserWithUsername(User.Identity.Name);

            //add the request to the FileRequestList
            Helper.FileRequests.Add(new FileRequest { ApplicationUser = applicationUser, DocumentType = documentType, Description = description });

            return View(viewModel);
        }
    }
}
