﻿using BenchmarkDotNet.Attributes;
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
        public bool IsExists_by_full_entity_projection()
        {
            using var dbcontext = factory.CreateDbContext();
            var data = dbcontext.Logs.Where(x => x.Id == 5).FirstOrDefault();
            return data != null;
        }

        [Benchmark]
        public bool IsExists_by_any()
        {
            using var dbcontext = factory.CreateDbContext();
            var isExists = dbcontext.Logs.Any(x => x.Id == 5);
            return isExists;
        }

        //[Benchmark]
        //public void Get_by_Parameterized_Query()
        //{
        //    int id = 5;
        //    using var dbcontext = factory.CreateDbContext();
        //    var data = dbcontext.Logs.Where(x=>x.Id==id).ToList();
        //}

        //[Benchmark]
        //public void Get_by_Constant_Query()
        //{
        //    using var dbcontext = factory.CreateDbContext();
        //    var data = dbcontext.Logs.Where(x => x.Id == 5).ToList();
        //}


        //[Benchmark]
        //public void Update_with_get_SaveChange()
        //{
        //    using var dbcontext = factory.CreateDbContext();
        //    dbcontext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
        //    var log = dbcontext.Logs.FirstOrDefault(x => x.Id == 1);
        //    log.Message = "Updating from get and save change";
        //    log.TimeStamp = DateTime.Now;
        //    dbcontext.SaveChanges();
        //}

        //[Benchmark]
        //public void Update_with_ExecuteUpdate()
        //{
        //    using var dbcontext = factory.CreateDbContext();
        //    dbcontext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
        //    var log = dbcontext.Logs
        //        .TagWith("Execute update testing")
        //        .Where(d => d.Id == 1)
        //        .ExecuteUpdate(x => x
        //        .SetProperty(p => p.Message, "Execute update")
        //        .SetProperty(p => p.TimeStamp, DateTime.Now)
        //        );
        //}

        //[Benchmark]
        //public void Get_With_Single_Factory_Instance()
        //{
        //    using var dbcontext = factory.CreateDbContext();
        //    var data = dbcontext.Logs.ToList();
        //}

        //[Benchmark]
        //public void Get_With_Multiple_Factory_Instance()
        //{
        //    var options = new DbContextOptionsBuilder<AppDBContext>()
        //                .UseSqlServer(@"Server=localhost;Database=SERILOG;Trusted_Connection=True;Encrypt=false")
        //                .Options;

        //    var poolFactory = new PooledDbContextFactory<AppDBContext>(options);
        //    using var dbcontext = poolFactory.CreateDbContext();
        //    var data = dbcontext.Logs.ToList();
        //}
    }
}