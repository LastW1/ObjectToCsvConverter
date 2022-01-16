using ObjectToCsvConverter.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestingProject
{
    class InvalidAttributeDto
    {
        [CsvConverterDate]
        public int InvalidDateField = 123123123;
    }
}
