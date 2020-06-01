using System;
using System.Data;
using Moq;

namespace ObjectMapperBenchMark.Extensions
{
    public static class DataReaderMockSetup
    {
        public static void Setup(this Mock<IDataReader> reader)
        {
            reader.Setup(m => m.FieldCount).Returns(2);
            reader.Setup(m => m.GetName(0)).Returns("Id");
            reader.Setup(m => m.GetName(1)).Returns("Name");

            reader.Setup(m => m.GetFieldType(0)).Returns(typeof(Guid));
            reader.Setup(m => m.GetFieldType(1)).Returns(typeof(string));

            reader.Setup(m => m.GetValue(0)).Returns(Guid.Empty);
            reader.Setup(m => m.GetValue(1)).Returns(string.Empty);

            reader.SetupSequence(m => m.Read())
                .Returns(true)
                .Returns(true)
                .Returns(false);
        }
    }
}
