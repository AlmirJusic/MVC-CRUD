using Database.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.EF
{
    public class mojDbContext:DbContext
    {
        public mojDbContext(DbContextOptions<mojDbContext>options):base(options)
        {
            
        }

        public DbSet<Champion> Champion { get; set; }
        public DbSet<Role> Role { get; set; }
    }
}
