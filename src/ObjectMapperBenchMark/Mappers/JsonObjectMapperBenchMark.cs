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
        private Consumer consumer;
        private Mock<IDataReader> reader;


        [GlobalSetup]
        public void GlobalSetup()
        {
            this.consumer = new Consumer();
            this.reader = new Mock<IDataReader>();
            this.reader.Setup();
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            this.reader = null;
        }

        [Benchmark]
        public void AsArray()
        {
            var dictionary = this.Serialize(this.reader.Object);
            var json = JsonConvert.SerializeObject(dictionary);
            var result = JsonConvert.DeserializeObject<IEnumerable<Model>>(json, Settings);
            result.Consume(this.consumer);
        }

        [Benchmark]
        public Model As()
        {
            var dictionary = this.Serialize(this.reader.Object).FirstOrDefault();
            var json = JsonConvert.SerializeObject(dictionary);
            var result = JsonConvert.DeserializeObject<Model>(json, Settings);
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

        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };
    }
}
