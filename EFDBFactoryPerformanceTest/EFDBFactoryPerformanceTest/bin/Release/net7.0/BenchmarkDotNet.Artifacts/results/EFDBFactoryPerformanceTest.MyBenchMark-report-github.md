```

BenchmarkDotNet v0.13.7, Windows 11 (10.0.22621.2134/22H2/2022Update/SunValley2)
AMD Ryzen 5 3500U with Radeon Vega Mobile Gfx, 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.100-preview.7.23376.3
  [Host]     : .NET 7.0.10 (7.0.1023.36312), X64 RyuJIT AVX2 [AttachedDebugger]
  DefaultJob : .NET 7.0.10 (7.0.1023.36312), X64 RyuJIT AVX2


```
|                             Method |     Mean |     Error |    StdDev |   Median |     Gen0 |     Gen1 | Allocated |
|----------------------------------- |---------:|----------:|----------:|---------:|---------:|---------:|----------:|
|   Get_With_Single_Factory_Instance | 2.844 ms | 0.0706 ms | 0.2003 ms | 2.754 ms | 253.9063 | 140.6250 |   1.01 MB |
| Get_With_Multiple_Factory_Instance | 3.779 ms | 0.0802 ms | 0.2248 ms | 3.665 ms | 289.0625 | 136.7188 |    1.1 MB |
