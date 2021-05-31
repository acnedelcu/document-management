using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Models
{
    public class ApplicationUserRepository: IApplicationUserRepository
    {
        private readonly AppDbContext appDbContext;
        public ApplicationUserRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public IEnumerable<ApplicationUser> AllApplicationUsers
        {
            get
            {
                return this.appDbContext.ApplicationUsers;
            }
        }

        public IEnumerable<ApplicationUser> GetUserByNames(string firstName, string lastName)
        {
            return this.appDbContext.ApplicationUsers.Where(au => au.FirstName == firstName && au.LastName == lastName);
        }

        public IEnumerable<ApplicationUser> GetUsersFromGroup(string firstName, string lastName, string group)
        {
            return this.appDbContext.ApplicationUsers.Where(au => au.FirstName == firstName && au.LastName == lastName);
        }

        public ApplicationUser GetUserWithId(string id)
        {
            return this.appDbContext.ApplicationUsers.Where(au => au.Id == id).FirstOrDefault();
        }
    }
}
