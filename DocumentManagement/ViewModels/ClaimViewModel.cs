using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.ViewModels
{
    public class ClaimViewModel
    {
        private const string errorMessage = "Camp obligatoriu";
        public ClaimViewModel()
        {
            ListDocType = new List<string> { "Adeverinta student", "Foaie matricola" };
        }

        public static List<string> ListDocType { get; set; }

        [Required(ErrorMessage = errorMessage)]
        public string DocumentType { get; set; }

        [Required(ErrorMessage = errorMessage)]
        public string Description { get; set; }
    }
}
