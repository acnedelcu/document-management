using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Models
{
    public interface IStudyProgramRepository
    {
        public IEnumerable<StudyProgram> AllStudyPrograms { get; }
    }
}
