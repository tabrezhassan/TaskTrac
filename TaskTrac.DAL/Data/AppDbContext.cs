using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTrac.DAL.Models;

namespace TaskTrac.DAL.Data
{
    public class AppDbContext : IdentityDbContext<Users>
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions options) : base(options) 
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=TaskTrac;Integrated Security=True; Encrypt=False");
        }

        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<SubTasks> SubTasks { get; set; }
    }
}
