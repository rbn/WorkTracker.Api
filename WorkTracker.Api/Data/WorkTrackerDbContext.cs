using Microsoft.EntityFrameworkCore;
using WorkTracker.Api.Models;

namespace WorkTracker.Api.Data
{
    public class WorkTrackerDbContext(DbContextOptions<WorkTrackerDbContext> options)
    : DbContext(options)
    {
        public DbSet<WorkItem> WorkItems { get; set; }
    }
}

