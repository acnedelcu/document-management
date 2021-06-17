using DocumentManagement.Models;
using DocumentManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public QrcodeController(IApplicationUserRepository applicationUserRepository, IGroupRepository groupRepository, IStudyProgramRepository studyProgramRepository)
        {
            this.applicationUserRepository = applicationUserRepository;
            this.groupRepository = groupRepository;
            this.studyProgramRepository = studyProgramRepository;
        }

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

        public IActionResult DownloadQr(GenerateViewModel viewModel)
        {
            byte[] qrCode;
            Bitmap generatedQrCode;
            using(var memoryStream = new MemoryStream())
            {
                if(viewModel == null)
                {
                    generatedQrCode = Helper.GenerateQrCodeForUrl(DocumentsListUrl);
                }else
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
    }
}
