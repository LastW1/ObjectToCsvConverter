using System;

namespace TestingProject
{
    class SimpleMixedValuesDto
    {
        public byte ByteValue = 1;
        public int IntValue = 1111;
        public double DoubleValue = 1234.12;
        public float FloatValue = 1234.12f;
        public decimal DecimalValue = 123.12m;
        public int? NullableNullValue = null;
        public DateTime DateValue = DateTime.Now;
        public string StringLooksLikeDate = "3-6";
    }
}
