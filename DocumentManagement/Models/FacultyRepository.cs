using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Models
{
    public class FacultyRepository: IFacultyRepository
    {
        private readonly AppDbContext appDbContext;
        public FacultyRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;    
        }
        public IEnumerable<Faculty> AllFaculties
        {
            get
            {
                return this.appDbContext.Faculties;
            }
        }
    }
}
