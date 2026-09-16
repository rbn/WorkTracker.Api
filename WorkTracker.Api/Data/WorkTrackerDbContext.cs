using Microsoft.EntityFrameworkCore;

namespace WorkTracker.Api.Data
{
    public class WorkTrackerDbContext : DbContext
    {
        public WorkTrackerDbContext(DbContextOptions<WorkTrackerDbContext> options) : base(options)
        {
        }
        public DbSet<Models.WorkItem> WorkItems { get; set; }
    }
}
