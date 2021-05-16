using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Models
{
    public class AppDbContext: IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Faculty> Faculties { get; set; }

        public DbSet<StudyProgram> StudyPrograms { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
