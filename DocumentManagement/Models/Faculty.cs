using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Models
{
    public class Faculty
    {
        public int FacultyId { get; set; }

        public string Name { get; set; }

        public List<StudyProgram> StudyPrograms { get; set; }

        public List<Student> Students { get; set; }

    }
}
