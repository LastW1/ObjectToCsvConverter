using System;
using System.Collections.Generic;

namespace TestingProject
{
    class Program
    {
        static void Main(string[] args)
        {
            /*  var simpleDto = new SimpleDto
              {
                  Field1 = "Field1",
                  Field2 = "Field2",
                  Field3 = "Field3"
              };

              ObjectToCsvConverter.ObjectToCsvConverter converter = new ObjectToCsvConverter.ObjectToCsvConverter(simpleDto);

              var lol = converter.ConvertToString();

              var simpleDto2 = new List<SimpleDto>
              {
                   new SimpleDto
                   {
                      Field1 = "Field1",
                      Field2 = "Field2",
                      Field3 = "Field3"
                   }
              };

              converter.ObjectToConvert = simpleDto2;

              lol = converter.ConvertToString();*/

              var simpleDto2 = new List<SimpleDto>
                {
                     new SimpleDto
                     {
                        Field1 = "Field1",
                        Field2 = "Field2",
                        Field3 = "Field3"
                     },
                      new SimpleDto
                     {
                        Field1 = "Field4",
                        Field2 = "Field5",
                        Field3 = "Field6"
                     }
                };     

            ObjectToCsvConverter.ObjectToCsvConverter converter = new ObjectToCsvConverter.ObjectToCsvConverter(simpleDto2);

            converter.ObjectToConvert = simpleDto2;

            converter.ConvertToXlsx("test");

            Console.WriteLine("Hello World!");
        }
    }
}
