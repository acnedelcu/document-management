using DocumentManagement.BlobStorage;
using DocumentManagement.Models;
using DocumentManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Controllers
{
    [Authorize]
    public class QrcodeController : Controller
    {
        private static readonly string GeneratedQrFileName = "AccountQrCode.bmp";
        private static readonly string DocumentsListUrl = "Document/List";

        private readonly IApplicationUserRepository applicationUserRepository;
        private readonly IGroupRepository groupRepository;
        private readonly IStudyProgramRepository studyProgramRepository;
        private readonly IConfiguration configuration;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="applicationUserRepository"></param>
        /// <param name="groupRepository"></param>
        /// <param name="studyProgramRepository"></param>
        /// <param name="configuration"></param>
        public QrcodeController(IApplicationUserRepository applicationUserRepository, IGroupRepository groupRepository, IStudyProgramRepository studyProgramRepository, IConfiguration configuration)
        {
            this.applicationUserRepository = applicationUserRepository;
            this.groupRepository = groupRepository;
            this.studyProgramRepository = studyProgramRepository;
            this.configuration = configuration;
        }

        /// <summary>
        /// Action method redirecting the user based on their role
        /// </summary>
        /// <returns></returns>
        public IActionResult Generate()
        {
            if (User.IsInRole("Admin"))
            {
                GenerateViewModel generateAdminViewModel = new GenerateViewModel
                { ApplicationUsers = applicationUserRepository.AllApplicationUsers, Groups = groupRepository.AllGroups, StudyPrograms = studyProgramRepository.AllStudyPrograms };
                return View(generateAdminViewModel);
            }
            else if (User.IsInRole("User"))
            {
                return View();
            }
            else return Forbid(); //return 403 forbidden
        }

        /// <summary>
        /// Downloads the generated QR code containing the url pointing to the list of available documents
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "User")]
        public IActionResult DownloadQr(GenerateViewModel viewModel)
        {
            byte[] qrCode;
            Bitmap generatedQrCode;
            using (var memoryStream = new MemoryStream())
            {
                if (viewModel == null)
                {
                    generatedQrCode = Helper.GenerateQrCodeForUrl(DocumentsListUrl);
                }
                else
                {
                    generatedQrCode = Helper.GenerateQrCodeForUrl(DocumentsListUrl);
                }
                generatedQrCode.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Bmp);
                qrCode = memoryStream.ToArray();
            }
            var content = new System.IO.MemoryStream(qrCode);
            var contentType = "APPLICATION/octet-stream";
            var fileName = GeneratedQrFileName;

            return File(content, contentType, fileName);
        }

        /// <summary>
        /// Downloads the generated QR code containing encoded url with longer lifetime
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult DownloadArchiveQr(ListViewModel viewModel)
        {
            string selectedFileName = viewModel.SelectedFileName;
            string username = viewModel.Username;
            ApplicationUser applicationUser = applicationUserRepository.GetUserWithUsername(username);
            FileHandler fileHandler = new FileHandler(configuration);

            string blobUrl = fileHandler.GenerateBlobSasUrl(applicationUser, selectedFileName);

            //Generate and download the QR code
            byte[] qrCode;
            using (var memoryStream = new MemoryStream())
            {
                Bitmap generatedQrCode = Helper.GenerateQrCodeForUrl(blobUrl);
                generatedQrCode.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Bmp);
                qrCode = memoryStream.ToArray();
            }
            var content = new System.IO.MemoryStream(qrCode);
            var contentType = "APPLICATION/octet-stream";
            var fileName = GeneratedQrFileName;

            return File(content, contentType, fileName);
        }
    }
}
