using System;
using System.Collections.Generic;
using System.Text;
using ObjectToCsvConverter.Attributes;

namespace TestingProject
{
    class AttributesDto
    {
        [CsvConverterDate]
        public DateTime DateWithAttribute = DateTime.Now;

        [CsvConverterDate]
        public DateTime? NullableDateWithAttribute = DateTime.Now;
    }
}
