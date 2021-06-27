using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string LastName { get; set; }

        public string DadFirstNameInitial { get; set; }

        public string FirstName { get; set; }

        public string EnrollmentNumber { get; set; }

        public string ContainerGuid { get; set; }

        public List<Faculty> Faculties { get; set; }

        public List<Group> Groups { get; set; }
    }
}
