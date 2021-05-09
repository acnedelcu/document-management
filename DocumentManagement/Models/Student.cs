using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        public string LastName { get; set; }

        public string DadFirstNameInitial { get; set; }

        public string FirtstName { get; set; }

        public DateTime BirthDate { get; set; }

        public string SocialSecurityNumber { get; set; }

        public List<Faculty> Faculties { get; set; }

        public List<Group> Groups { get; set; }
    }
}
