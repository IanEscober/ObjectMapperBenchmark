# Object Mapper Benchmark
A simple benchmark using [BenchmarkDotNet](https://github.com/dotnet/BenchmarkDotNet) for the different techniques of mapping database data to objects.

## Mapping Techniques
1. Json Serialize-Deserialize - Uses [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/) library to serialize a dictionary of property names and its value then deserialzing it to an object.
2. Reflection - Hand made plain old reflection.
3. FastMember Reflection - Uses Marc Gravell's [FastMember Library](https://github.com/mgravell/fast-member) that improves reflection performance.

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

    public string String { get; set; } // Mock = Hello World

    public DateTime Date { get; set; } // Mock = DateTime.UtcNow
}
```

## Results
![Bechmark Results](https://github.com/IanEscober/ObjectMapperBenchmark/blob/master/docs/Results.JPG)

- __Fastest Mapping is by Json Serialize-Deserialize__ which is 4x faster than the other two techniques on mapping multiple rows but a marginal difference on single row mapping.

## Contribution
Yeet a Pull Request

## License
[MIT](https://github.com/IanEscober/ObjectMapperBenchmark/blob/master/License)