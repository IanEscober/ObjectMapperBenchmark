``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18362.836 (1903/May2019Update/19H1)
Intel Core i7-3630QM CPU 2.40GHz (Ivy Bridge), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=2.2.110
  [Host]     : .NET Core 2.2.8 (CoreCLR 4.6.28207.03, CoreFX 4.6.28208.02), X64 RyuJIT
  Job-KJGQXT : .NET Core 2.2.8 (CoreCLR 4.6.28207.03, CoreFX 4.6.28208.02), X64 RyuJIT

InvocationCount=1  UnrollFactor=1  

```
|                            Type |  Method |  Rows |           Mean |        Error |       StdDev |         Median |
|-------------------------------- |-------- |------ |---------------:|-------------:|-------------:|---------------:|
| **FastMemberObjectMapperBenchMark** |      **As** |    **10** |       **863.6 μs** |     **15.19 μs** |     **12.68 μs** |       **870.5 μs** |
|       JsonObjectMapperBenchMark |      As |    10 |       695.6 μs |     13.20 μs |     30.60 μs |       691.0 μs |
| ReflectionObjectMapperBenchMark |      As |    10 |       944.5 μs |     16.96 μs |     28.80 μs |       941.9 μs |
| FastMemberObjectMapperBenchMark | AsArray |    10 |     8,395.1 μs |    165.23 μs |    220.57 μs |     8,354.2 μs |
|       JsonObjectMapperBenchMark | AsArray |    10 |     2,671.5 μs |     53.26 μs |    137.47 μs |     2,609.4 μs |
| ReflectionObjectMapperBenchMark | AsArray |    10 |     8,392.6 μs |    144.47 μs |    128.07 μs |     8,419.9 μs |
| **FastMemberObjectMapperBenchMark** |      **As** |   **100** |       **860.2 μs** |     **17.08 μs** |     **21.60 μs** |       **857.1 μs** |
|       JsonObjectMapperBenchMark |      As |   100 |       739.3 μs |     23.16 μs |     66.82 μs |       715.5 μs |
| ReflectionObjectMapperBenchMark |      As |   100 |       954.6 μs |     18.09 μs |     44.36 μs |       940.7 μs |
| FastMemberObjectMapperBenchMark | AsArray |   100 |    83,540.8 μs |    436.69 μs |    408.48 μs |    83,499.5 μs |
|       JsonObjectMapperBenchMark | AsArray |   100 |    22,165.1 μs |    437.49 μs |    409.23 μs |    22,076.4 μs |
| ReflectionObjectMapperBenchMark | AsArray |   100 |    83,895.8 μs |    589.42 μs |    551.34 μs |    83,838.1 μs |
| **FastMemberObjectMapperBenchMark** |      **As** | **10000** |       **990.7 μs** |     **19.81 μs** |     **40.47 μs** |       **997.6 μs** |
|       JsonObjectMapperBenchMark |      As | 10000 |       835.3 μs |     16.52 μs |     35.57 μs |       832.1 μs |
| ReflectionObjectMapperBenchMark |      As | 10000 |     1,080.0 μs |     21.42 μs |     50.07 μs |     1,090.9 μs |
| FastMemberObjectMapperBenchMark | AsArray | 10000 | 8,298,352.8 μs | 43,828.76 μs | 38,853.05 μs | 8,294,833.4 μs |
|       JsonObjectMapperBenchMark | AsArray | 10000 | 2,148,111.2 μs | 27,813.83 μs | 23,225.81 μs | 2,136,445.5 μs |
| ReflectionObjectMapperBenchMark | AsArray | 10000 | 8,408,124.8 μs | 80,225.57 μs | 75,043.05 μs | 8,376,021.8 μs |
