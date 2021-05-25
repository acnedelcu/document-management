using DocumentManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.ViewModels
{
    public class UploadViewModel
    {
        public IEnumerable<Group> Groups { get; set; }
    }
}
