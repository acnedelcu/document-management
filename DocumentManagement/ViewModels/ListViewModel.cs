using DocumentManagement.BlobStorage;
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
    }
}
