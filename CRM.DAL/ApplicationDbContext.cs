using CRM.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Test.Models;

namespace Test.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Notes> Notes { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<Wedo> Wedo { get; set; }
        public DbSet<Blog> Blog { get; set; }
        public DbSet<About> About { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

    }
}