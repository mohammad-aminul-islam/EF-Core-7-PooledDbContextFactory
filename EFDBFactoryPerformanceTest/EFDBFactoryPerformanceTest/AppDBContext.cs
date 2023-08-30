using Microsoft.EntityFrameworkCore;

namespace EFDBFactoryPerformanceTest
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) :
            base(options)
        {

        }

        public DbSet<Log> Logs { get; set; }
    }
}