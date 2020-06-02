using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Moq;
using Newtonsoft.Json;
using ObjectMapperBenchMark.Extensions;

namespace ObjectMapperBenchMark.Mappers
{
    public class JsonObjectMapperBenchMark
    {
        private const int ROWS = 10000;
        private Mock<IDataReader> reader;
        private Consumer consumer;

        [GlobalSetup]
        public void GlobalSetup()
        {
            this.reader = new Mock<IDataReader>();
            this.consumer = new Consumer();
            this.reader.Setup();
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            this.reader = null;
        }

        [IterationSetup]
        public void IterationSetup()
        {
            this.reader.SetupRows(ROWS);
        }

        [Benchmark]
        public Model As()
        {
            var result = this.AsJson<Model>(this.reader.Object);
            return result;
        }

        [Benchmark]
        public void AsArray()
        {
            var result = this.AsArrayJson<IEnumerable<Model>>(this.reader.Object);
            result.Consume(this.consumer);
        }

        private T AsArrayJson<T>(IDataReader reader) where T : IEnumerable
        {
            var dictionary = this.Serialize(reader);
            var json = JsonConvert.SerializeObject(dictionary);
            var result = JsonConvert.DeserializeObject<T>(json, this.Settings);
            return result;
        }

        private T AsJson<T>(IDataReader reader) where T : class
        {
            var dictionary = this.Serialize(reader).FirstOrDefault();
            var json = JsonConvert.SerializeObject(dictionary);
            var result = JsonConvert.DeserializeObject<T>(json, this.Settings);
            return result;
        }

        private IEnumerable<Dictionary<string, object>> Serialize(IDataReader reader)
        {
            var results = new List<Dictionary<string, object>>();
            var cols = new List<string>();
            for (var i = 0; i < reader.FieldCount; i++)
            {
                cols.Add(reader.GetName(i));
            }

            while (reader.Read())
            {
                results.Add(this.SerializeRow(cols, reader));
            }

            return results;
        }

        private Dictionary<string, object> SerializeRow(IEnumerable<string> cols, IDataReader reader)
        {
            var result = new Dictionary<string, object>();
            foreach (var col in cols)
            {
                result.Add(col, reader[col]);
            }

            return result;
        }

        private readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };
    }
}
