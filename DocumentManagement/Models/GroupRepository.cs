using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Models
{
    public class GroupRepository:IGroupRepository
    {
        private readonly AppDbContext appDbContext;

        public GroupRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public IEnumerable<Group> AllGroups
        {
            get
            {
                return this.appDbContext.Groups;
            }
        }
    }
}
