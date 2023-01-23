using DSRNetSchool.Context.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSRNetSchool.Context
{
    public class MainDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
