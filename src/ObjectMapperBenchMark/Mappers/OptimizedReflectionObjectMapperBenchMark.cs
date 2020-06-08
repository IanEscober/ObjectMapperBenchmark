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
    public class OptimizedReflectionObjectMapperBenchMark
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
            var result = this.AsOptimizedReflection<Model>(this.reader.Object);
            return result;
        }

        [Benchmark]
        public void AsArray()
        {
            var result = this.AsArrayOptimizedReflection<Model>(this.reader.Object);
            result.Consume(this.consumer);
        }

        private T AsOptimizedReflection<T>(IDataReader reader) where T : class, new()
        {
            var properties = this.CacheProperties<T>(reader);
            var result = this.MapToProperties(properties, reader);
            return result;
        }

        private IEnumerable<T> AsArrayOptimizedReflection<T>(IDataReader reader) where T : class, new()
        {
            var results = new List<T>(reader.FieldCount);
            var properties = this.CacheProperties<T>(reader);

            while (reader.Read())
            {
                results.Add(this.MapToProperties(properties, reader));
            }

            return results;
        }

        private Dictionary<string, Action<T, object>> CacheProperties<T>(IDataReader reader) where T : class, new()
        {
            var properties = new Dictionary<string, Action<T, object>>();

            for (int i = 0; i < reader.FieldCount; i++)
            {
                var fieldName = reader.GetName(i);
                var methodInfo = typeof(T).GetProperty(reader.GetName(i))?.GetSetMethod();

                if (methodInfo != null)
                {
                    var setter = ReflectionOptimizer.CreateDowncastDelegate<T>(methodInfo);
                    properties.Add(fieldName, setter);
                }
            }

            return properties;
        }

        private T MapToProperties<T>(Dictionary<string, Action<T, object>> properties, IDataReader reader) where T : class, new()
        {
            var result = new T();

            foreach (var property in properties)
            {
                var value = reader[property.Key];
                if (value == DBNull.Value)
                {
                    value = null;
                }
                property.Value(result, value);
            }

            return result;
        }

        internal static class ReflectionOptimizer
        {
            public static Action<T, object> CreateDowncastDelegate<T>(MethodInfo method)
            {
                MethodInfo openImpl = typeof(ReflectionOptimizer).GetMethod(nameof(CreateDowncastDelegateImpl));
                MethodInfo closedImpl = openImpl.MakeGenericMethod(typeof(T), method.GetParameters()[0].ParameterType);
                return (Action<T, object>)closedImpl.Invoke(null, new object[] { method });
            }

            public static Action<TSource, object> CreateDowncastDelegateImpl<TSource, TParam>(MethodInfo method)
            {
                object tdelegate = Delegate.CreateDelegate(typeof(Action<TSource, TParam>), null, method);
                Action<TSource, TParam> call = (Action<TSource, TParam>)tdelegate;
                return delegate (TSource source, object parameter) { call(source, (TParam)parameter); };
            }
        }
    }
}
