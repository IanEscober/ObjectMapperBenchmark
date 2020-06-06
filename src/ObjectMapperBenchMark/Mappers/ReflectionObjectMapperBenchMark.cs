using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Moq;
using ObjectMapperBenchMark.Extensions;

namespace ObjectMapperBenchMark.Mappers
{
    public class ReflectionObjectMapperBenchMark
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
            var result = this.AsReflection<Model>(this.reader.Object);
            return result;
        }

        [Benchmark]
        public void AsArray()
        {
            var result = this.AsArrayReflection<Model>(this.reader.Object);
            result.Consume(this.consumer);
        }

        private T AsReflection<T>(IDataReader reader) where T : class, new()
        {
            var result = new T();
            var resultType = typeof(T);
            var properties = resultType.GetProperties();

            for (int i = 0; i < reader.FieldCount; i++)
            {
                var propertyName = reader.GetName(i);
                var value = reader[propertyName] == DBNull.Value ? null : reader[propertyName];
                resultType.GetProperty(propertyName).SetValue(result, value);
            }

            return result;
        }

        private IEnumerable<T> AsArrayReflection<T>(IDataReader reader) where T : class, new()
        {
            var results = new List<T>();
            var result = new T();
            var resultType = typeof(T);
            var properties = resultType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    var propertyName = reader.GetName(i);
                    var value = reader[propertyName] == DBNull.Value ? null : reader[propertyName];
                    resultType.GetProperty(propertyName).SetValue(result, value);
                }

                results.Add(result);
            }

            return results;
        }
    }
}
