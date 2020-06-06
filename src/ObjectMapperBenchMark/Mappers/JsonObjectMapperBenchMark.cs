using System.Collections;
using System.Collections.Generic;
using System.Data;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Moq;
using Newtonsoft.Json;
using ObjectMapperBenchMark.Extensions;

namespace ObjectMapperBenchMark.Mappers
{
    public class JsonObjectMapperBenchMark
    {
        private Mock<IDataReader> reader;
        private Consumer consumer;

        [Params(10, 100, 10000)]
        public int Rows { get; set; }

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
            this.reader.SetupRows(this.Rows);
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
            var rows = new List<Dictionary<string, object>>();
            var cols = this.GetRowNames(reader);
            while (reader.Read())
            {
                rows.Add(this.SerializeRow(cols, reader));
            }
            var json = JsonConvert.SerializeObject(rows);
            var result = JsonConvert.DeserializeObject<T>(json, this.Settings);
            return result;
        }

        private T AsJson<T>(IDataReader reader) where T : class
        {
            var cols = this.GetRowNames(reader);
            var row = this.SerializeRow(cols, reader);
            var json = JsonConvert.SerializeObject(row);
            var result = JsonConvert.DeserializeObject<T>(json, this.Settings);
            return result;
        }

        private IEnumerable<string> GetRowNames(IDataReader reader)
        {
            var result = new List<string>();
            for (var i = 0; i < reader.FieldCount; i++)
            {
                result.Add(reader.GetName(i));
            }

            return result;
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
