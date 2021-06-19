using DocumentManagement.BlobStorage;
using DocumentManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.ViewModels
{
    public class ListViewModel
    {
        public ListViewModel()
        {
            BlobNames = new List<DatagridFileWrapper>();
        }
        public List<DatagridFileWrapper> BlobNames { get; set; }

        public string SelectedFileName { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public string Username { get; set; }
    }
}
