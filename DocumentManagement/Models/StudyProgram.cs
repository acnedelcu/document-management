using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Models
{
    public class StudyProgram
    {
        public int StudyProgramId { get; set; }

        public string DegreeType { get; set; }

        public string Major { get; set; }

        public bool DistanceLearning { get; set; }

    }
}
