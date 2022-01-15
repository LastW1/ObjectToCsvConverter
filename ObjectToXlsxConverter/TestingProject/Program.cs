using System;

namespace TestingProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var simpleDto = new SimpleDto
            {
                Field1 = "Field1",
                Field2 = "Field2",
                Field3 = "Field3"
            };

            ObjectToXlsxConverter.ObjectToXlsxConverter converter = new ObjectToXlsxConverter.ObjectToXlsxConverter(simpleDto);

            var lol = converter.ConvertToString();

            Console.WriteLine("Hello World!");
        }
    }
}
