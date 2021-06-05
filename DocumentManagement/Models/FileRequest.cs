using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Models
{
    public class FileRequest
    {
        public ApplicationUser ApplicationUser { get; set; }

        public string DocumentType { get; set; }

        public string Description { get; set; }

    }
}
