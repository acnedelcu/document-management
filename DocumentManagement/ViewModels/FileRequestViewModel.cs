using DocumentManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.ViewModels
{
    public class FileRequestViewModel
    {
        public FileRequestViewModel()
        {
            this.FileRequests = Helper.FileRequests;
        }

        public List<FileRequest> FileRequests { get; set; }
    }
}
