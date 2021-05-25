using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Models
{
    public class StudyProgramRepository: IStudyProgramRepository
    {
        private readonly AppDbContext appDbContext;
        public StudyProgramRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public IEnumerable<StudyProgram> AllStudyPrograms
        {
            get
            {
                return this.appDbContext.StudyPrograms;
            }
        }
    }
}
