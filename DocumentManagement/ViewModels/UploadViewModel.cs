using DocumentManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.ViewModels
{
    public class UploadViewModel
    {
        public IEnumerable<Group> Groups { get; set; }

        public IEnumerable<StudyProgram> StudyPrograms { get; set; } 

        public IEnumerable<ApplicationUser> ApplicationUsers { get; set; }

        [Required(ErrorMessage = "Camp obligatoriu!")]
        [Display(Name = "Prenume")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Nume")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Grupa")]
        public string Group { get; set; }
    }
}
