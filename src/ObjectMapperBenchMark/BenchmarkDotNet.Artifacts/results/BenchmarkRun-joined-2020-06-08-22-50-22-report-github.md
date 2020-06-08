``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18362.836 (1903/May2019Update/19H1)
Intel Core i7-3630QM CPU 2.40GHz (Ivy Bridge), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=2.2.110
  [Host]     : .NET Core 2.2.8 (CoreCLR 4.6.28207.03, CoreFX 4.6.28208.02), X64 RyuJIT
  Job-SJTQXC : .NET Core 2.2.8 (CoreCLR 4.6.28207.03, CoreFX 4.6.28208.02), X64 RyuJIT

InvocationCount=1  UnrollFactor=1  

```
|                                     Type |  Method |  Rows |           Mean |        Error |       StdDev |
|----------------------------------------- |-------- |------ |---------------:|-------------:|-------------:|
|          **FastMemberObjectMapperBenchMark** |      **As** |    **10** |       **828.4 μs** |     **15.50 μs** |     **26.32 μs** |
|                JsonObjectMapperBenchMark |      As |    10 |       692.2 μs |     13.71 μs |     31.23 μs |
| OptimizedReflectionObjectMapperBenchMark |      As |    10 |       933.8 μs |     18.56 μs |     19.06 μs |
|          ReflectionObjectMapperBenchMark |      As |    10 |       868.8 μs |     13.24 μs |     11.06 μs |
|          FastMemberObjectMapperBenchMark | AsArray |    10 |     7,992.4 μs |    155.57 μs |    172.92 μs |
|                JsonObjectMapperBenchMark | AsArray |    10 |     2,593.4 μs |     50.87 μs |     47.59 μs |
| OptimizedReflectionObjectMapperBenchMark | AsArray |    10 |     2,837.4 μs |     56.63 μs |     91.45 μs |
|          ReflectionObjectMapperBenchMark | AsArray |    10 |     8,033.6 μs |    157.33 μs |    174.87 μs |
|          **FastMemberObjectMapperBenchMark** |      **As** |   **100** |       **826.2 μs** |     **16.26 μs** |     **35.00 μs** |
|                JsonObjectMapperBenchMark |      As |   100 |       727.4 μs |     20.34 μs |     57.69 μs |
| OptimizedReflectionObjectMapperBenchMark |      As |   100 |       940.7 μs |     18.62 μs |     17.42 μs |
|          ReflectionObjectMapperBenchMark |      As |   100 |       871.1 μs |     16.90 μs |     13.20 μs |
|          FastMemberObjectMapperBenchMark | AsArray |   100 |    77,480.8 μs |    227.60 μs |    201.76 μs |
|                JsonObjectMapperBenchMark | AsArray |   100 |    21,849.9 μs |    247.75 μs |    193.43 μs |
| OptimizedReflectionObjectMapperBenchMark | AsArray |   100 |    21,017.3 μs |     89.54 μs |     79.38 μs |
|          ReflectionObjectMapperBenchMark | AsArray |   100 |    78,895.3 μs |    295.96 μs |    262.36 μs |
|          **FastMemberObjectMapperBenchMark** |      **As** | **10000** |       **950.6 μs** |     **18.82 μs** |     **19.32 μs** |
|                JsonObjectMapperBenchMark |      As | 10000 |       815.5 μs |     16.24 μs |     31.28 μs |
| OptimizedReflectionObjectMapperBenchMark |      As | 10000 |     1,114.6 μs |     22.19 μs |     52.31 μs |
|          ReflectionObjectMapperBenchMark |      As | 10000 |     1,040.1 μs |     20.29 μs |     32.76 μs |
|          FastMemberObjectMapperBenchMark | AsArray | 10000 | 7,665,265.0 μs | 61,077.52 μs | 47,685.31 μs |
|                JsonObjectMapperBenchMark | AsArray | 10000 | 2,106,959.5 μs |  4,782.32 μs |  3,733.72 μs |
| OptimizedReflectionObjectMapperBenchMark | AsArray | 10000 | 2,026,914.6 μs | 21,725.13 μs | 18,141.47 μs |
|          ReflectionObjectMapperBenchMark | AsArray | 10000 | 7,798,628.1 μs | 85,820.13 μs | 80,276.21 μs |
