using DocumentManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.ViewModels
{
    public class GenerateViewModel
    {
        private const string FormFieldErrorMessage = "Camp obligatoriu!";
        private const string FirstNameDisplayName = "Prenume", LastNameDisplayName = "Nume", DadNameInitialDisplayName = "Initiala tatalui",
            GroupDisplayName = "Grupa";
        public IEnumerable<Group> Groups { get; set; }

        public IEnumerable<StudyProgram> StudyPrograms { get; set; }

        public IEnumerable<ApplicationUser> ApplicationUsers { get; set; }

        [Required(ErrorMessage = FormFieldErrorMessage)]
        [Display(Name = FirstNameDisplayName)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = FormFieldErrorMessage)]
        [Display(Name = LastNameDisplayName)]
        public string LastName { get; set; }
        
        [Required(ErrorMessage = FormFieldErrorMessage)]
        [Display(Name = DadNameInitialDisplayName)]
        public string DadNameInitial { get; set; }

        [Required(ErrorMessage = FormFieldErrorMessage)]
        [Display(Name = GroupDisplayName)]
        public string Group { get; set; }
    }
}
