using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Models
{
    public class Group
    {
        public int GroupId { get; set; }

        public string GroupName { get; set; }

        public List<Student> Students { get; set; }
    }
}
