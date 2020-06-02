using System;
using System.Collections.Generic;
using System.Data;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using FastMember;
using Moq;
using ObjectMapperBenchMark.Extensions;

namespace ObjectMapperBenchMark.Mappers
{
    public class FastMemberObjectMapperBenchMark
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
            var result = this.AsFastMember<Model>(this.reader.Object);
            return result;
        }

        [Benchmark]
        public void AsArray()
        {
            var result = this.AsArrayFastMember<Model>(this.reader.Object);
            result.Consume(this.consumer);
        }

        private T AsFastMember<T>(IDataReader reader) where T : class, new()
        {
            var result = new T();
            var typeAccessor = TypeAccessor.Create(typeof(T));

            for (int i = 0; i < reader.FieldCount; i++)
            {
                var propertyName = reader.GetName(i);
                typeAccessor[result, propertyName] = reader[propertyName] == DBNull.Value ? null : reader[propertyName];
            }

            return result;
        }

        private IEnumerable<T> AsArrayFastMember<T>(IDataReader reader) where T : class, new()
        {
            var results = new List<T>();
            var result = new T();
            var typeAccessor = TypeAccessor.Create(typeof(T));

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    var propertyName = reader.GetName(i);
                    typeAccessor[result, propertyName] = reader[propertyName] == DBNull.Value ? null : reader[propertyName];
                }

                results.Add(result);
            }

            return results;
        }
    }
}
