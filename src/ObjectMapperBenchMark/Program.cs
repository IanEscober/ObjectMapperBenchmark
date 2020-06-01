using BenchmarkDotNet.Running;
using ObjectMapperBenchMark.Mappers;

namespace ObjectMapperBenchMark
{
    public class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<JsonObjectMapperBenchMark>();
        }
    }
}
