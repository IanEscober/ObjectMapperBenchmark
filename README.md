# Object Mapper Benchmark
A simple benchmark using [BenchmarkDotNet](https://github.com/dotnet/BenchmarkDotNet) for the different techniques of mapping database data to objects.

## Mapping Techniques
1. [Json Serialize-Deserialize](https://github.com/IanEscober/ObjectMapperBenchmark/blob/master/src/ObjectMapperBenchMark/Mappers/JsonObjectMapperBenchMark.cs) - Uses [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/) library to serialize a dictionary of property names and its value then deserialzing it to an object.
2. [Reflection](https://github.com/IanEscober/ObjectMapperBenchmark/blob/master/src/ObjectMapperBenchMark/Mappers/ReflectionObjectMapperBenchMark.cs) - Hand made plain old reflection.
3. [FastMember Reflection](https://github.com/IanEscober/ObjectMapperBenchmark/blob/master/src/ObjectMapperBenchMark/Mappers/FastMemberObjectMapperBenchMark.cs) - Uses Marc Gravell's [FastMember Library](https://github.com/mgravell/fast-member) that improves reflection performance.
4. [Optimized Reflection](https://github.com/IanEscober/ObjectMapperBenchmark/blob/master/src/ObjectMapperBenchMark/Mappers/OptimizedReflectionObjectMapperBenchMark.cs) - Inspired by [Google ProtoBuf](https://github.com/protocolbuffers/protobuf) which caches object setter to drastically improve mapping performance.

## Methods
1. `As()` - Maps a single row to an object.
2. `AsArray()` - Maps all the rown to a list of objects.

## Rows
The methods of each mapping technique are run on __10, 100, 10000 rows__.

## Model
```csharp
public class Model
{
    public Guid Id { get; set; } // Mock = Guid.NewGuid()

    public int Integer { get; set; } // Mock = int.MaxValue

    public decimal Decimal { get; set; } // Mock = decimal.MaxValue

    public string String { get; set; } // Mock = null

    public DateTime Date { get; set; } // Mock = DateTime.UtcNow
}
```

## Results
![Bechmark Results](https://github.com/IanEscober/ObjectMapperBenchmark/blob/master/docs/Results.JPG)

- __In mapping single rows, all techniques are very close to each other__. Json Serialize-Deserialize happens to have the fastest time but it is frankly small, in 100ths of a microsecond which barely affects performance in practice.
- __In mapping multiple rows the performance competition can be narrowed down to two (2) techniques, Json Serialize-Deserialize and Optimized Reflection__. Plain old reflection and FastMember are 4x slower than the latter.
- At 10 rows Json Serialize-Deserialize is faster than Optimized Reflection by ~8 %. As the magnitude of rows increase by 10 (100, 10000), Optimized Reflection seems to be faster at ~3 %.

## Conclusion
- At single row mapping all techniques pratically performs the same.
- At multiple row mapping either Json Serialize-Deserialize or Optimized Reflection are good options. The choice will just depend on usage and overhead since [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/) is a whole library compared to reflection which is built in.

## Contribution
Yeet a Pull Request

## License
[MIT](https://github.com/IanEscober/ObjectMapperBenchmark/blob/master/License)