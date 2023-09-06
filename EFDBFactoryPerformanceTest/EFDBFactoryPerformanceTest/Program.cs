using BenchmarkDotNet.Running;
using EFDBFactoryPerformanceTest;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;

BenchmarkRunner.Run<MyBenchMark>();
