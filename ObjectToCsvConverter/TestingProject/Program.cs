using System;
using System.Collections.Generic;
using System.Linq;

namespace TestingProject
{
    class Program
    {
        static void Main(string[] args)
        {
            ObjectToCsvConverter.ObjectToCsvConverter converter = new ObjectToCsvConverter.ObjectToCsvConverter(new object());

            //SimpleStringFieldsDtoTest(converter);
            //SimpleStringFieldsDtoInCollectionTest(converter);
            SimpleMixedValuesDtoTest(converter);

            Console.WriteLine("Hello World!");
        }

        private static void SimpleStringFieldsDtoTest(ObjectToCsvConverter.ObjectToCsvConverter converter)
        {
            var simpleDto = new SimpleStringFieldsDto
            {
                Field1 = "Field1",
                Field2 = "Field2",
                Field3 = "Field3"
            };

            converter.ObjectToConvert = simpleDto;

            converter.ConvertToCsv(nameof(SimpleStringFieldsDtoTest));
        }

        private static void SimpleStringFieldsDtoInCollectionTest(ObjectToCsvConverter.ObjectToCsvConverter converter)
        {
            var simpleDtoList = new List<SimpleStringFieldsDto>
                {
                     new SimpleStringFieldsDto
                     {
                        Field1 = "Field1",
                        Field2 = "Field2",
                        Field3 = "Field3"
                     },
                      new SimpleStringFieldsDto
                     {
                        Field1 = "Field4",
                        Field2 = "Field5",
                        Field3 = "Field6"
                     }
                };

            converter.ObjectToConvert = simpleDtoList;
            converter.ConvertToCsv(nameof(SimpleStringFieldsDtoInCollectionTest)+"AsList");

            var simpleDtoIList = (IList<SimpleStringFieldsDto>)simpleDtoList.AsEnumerable();
            converter.ObjectToConvert = simpleDtoIList;
            converter.ConvertToCsv(nameof(SimpleStringFieldsDtoInCollectionTest) + "AsIList");

            var simpleDtoEnumerable = simpleDtoList.AsEnumerable();
            converter.ObjectToConvert = simpleDtoEnumerable;
            converter.ConvertToCsv(nameof(SimpleStringFieldsDtoInCollectionTest) + "AsEnumerable");

            var simpleDtoQueryable = simpleDtoList.AsQueryable();
            converter.ObjectToConvert = simpleDtoQueryable;
            converter.ConvertToCsv(nameof(SimpleStringFieldsDtoInCollectionTest) + "AsQueryable");
        }

        private static void SimpleMixedValuesDtoTest(ObjectToCsvConverter.ObjectToCsvConverter converter)
        {
            converter.ObjectToConvert = new SimpleMixedValuesDto();
            converter.IsNullValueOverridedWithString = true;
            converter.ConvertToCsv(nameof(SimpleMixedValuesDtoTest));
        }
    }
}
