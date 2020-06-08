using System;
using System.Data;
using Moq;

namespace ObjectMapperBenchMark.Extensions
{
    public static class DataReaderMockSetup
    {
        public static void Setup(this Mock<IDataReader> reader)
        {
            reader.Setup(m => m.FieldCount).Returns(5);
            reader.Setup(m => m.GetName(0)).Returns("Id");
            reader.Setup(m => m.GetName(1)).Returns("Integer");
            reader.Setup(m => m.GetName(2)).Returns("Decimal");
            reader.Setup(m => m.GetName(3)).Returns("String");
            reader.Setup(m => m.GetName(4)).Returns("Date");

            reader.Setup(m => m["Id"]).Returns(Guid.NewGuid());
            reader.Setup(m => m["Integer"]).Returns(int.MaxValue);
            reader.Setup(m => m["Decimal"]).Returns(decimal.MaxValue);
            reader.Setup(m => m["String"]).Returns(DBNull.Value);
            reader.Setup(m => m["Date"]).Returns(DateTime.UtcNow);
        }

        public static void SetupRows(this Mock<IDataReader> reader, int rows)
        {
            var sequence = reader.SetupSequence(m => m.Read());

            for (int i = 0; i < rows; i++)
            {
                sequence.Returns(true);
            }

            sequence.Returns(false);
        }
    }
}
