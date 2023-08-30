using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EFDBFactoryPerformanceTest
{
    [MemoryDiagnoser]
    public class MyBenchMark
    {
        public DbContextOptions<AppDBContext>? Options { get; set; }
        public PooledDbContextFactory<AppDBContext>? factory { get; set; }

        [GlobalSetup]
        public void GlobalSetup()
        {
            Options = new DbContextOptionsBuilder<AppDBContext>()
                       .UseSqlServer(@"Server=localhost;Database=SERILOG;Trusted_Connection=True;Encrypt=false")
                       .Options;
            factory = new PooledDbContextFactory<AppDBContext>(Options);
        }

        [Benchmark]
        public void Get_With_Single_Factory_Instance()
        {
            var dbcontext = factory.CreateDbContext();
            var data = dbcontext.Logs.ToList();
            dbcontext.Dispose();
        }

        [Benchmark]
        public void Get_With_Multiple_Factory_Instance()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                        .UseSqlServer(@"Server=localhost;Database=SERILOG;Trusted_Connection=True;Encrypt=false")
                        .Options;

            var poolFactory = new PooledDbContextFactory<AppDBContext>(options);
            var dbcontext = poolFactory.CreateDbContext();
            var data = dbcontext.Logs.ToList();
            dbcontext.Dispose();
        }
    }
}