using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Diagnostics.Metrics;

namespace NetCoreWebApiMasterDetaileCRUD.Model
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options)
            :base(options)
        { 

        }
        public DbSet<student> students { get; set; }
        public DbSet<Details> details { get; set; }

    }
}
