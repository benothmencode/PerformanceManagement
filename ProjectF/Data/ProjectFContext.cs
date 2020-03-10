using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectF.Models;

namespace ProjectF.Data
{
    public class ProjectFContext : DbContext
    {
        public ProjectFContext (DbContextOptions<ProjectFContext> options)
            : base(options)
        {
        }

        public DbSet<ProjectF.Models.Badge> Badge { get; set; }
    }
}
