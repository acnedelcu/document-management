using DocumentManagement.Models;
using DocumentManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Controllers
{
    public class QrcodeController : Controller
    {
        private static readonly string GeneratedQrFileName = "AccountQrCode.bmp";
        private static readonly string DocumentsListUrl = "Document/List";

        private readonly IApplicationUserRepository applicationUserRepository;
        private readonly IGroupRepository groupRepository;
        private readonly IStudyProgramRepository studyProgramRepository;
        public QrcodeController(IApplicationUserRepository applicationUserRepository, IGroupRepository groupRepository, IStudyProgramRepository studyProgramRepository)
        {
            this.applicationUserRepository = applicationUserRepository;
            this.groupRepository = groupRepository;
            this.studyProgramRepository = studyProgramRepository;
        }

        public IActionResult Generate()
        {
            return View();
        }

        public IActionResult DownloadQr()
        {
            byte[] qrCode;
            using(var memoryStream = new MemoryStream())
            {
                var generatedQrCode = Helper.GenerateQrCodeForUrl(DocumentsListUrl);
                generatedQrCode.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Bmp);
                qrCode = memoryStream.ToArray();
            }
            var content = new System.IO.MemoryStream(qrCode);
            var contentType = "APPLICATION/octet-stream";
            var fileName = GeneratedQrFileName;

            return File(content, contentType, fileName);
        }

        public IActionResult GenerateAdmin()
        {
            var viewModel = new GenerateAdminViewModel
            {
                ApplicationUsers = this.applicationUserRepository.AllApplicationUsers,
                Groups = this.groupRepository.AllGroups,
                StudyPrograms = this.studyProgramRepository.AllStudyPrograms
            };
            return View(viewModel);
        }
    }
}
