using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Models
{
    public interface IGroupRepository
    {
        public IEnumerable<Group> AllGroups { get; }
    }
}
