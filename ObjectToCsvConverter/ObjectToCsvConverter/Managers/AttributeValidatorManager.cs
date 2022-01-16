using ObjectToCsvConverter.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ObjectToCsvConverter.Managers
{
    internal static class AttributeValidatorManager
    {
        private static List<Type> ValidDataTypes = new List<Type>
        {
            typeof(DateTime),
            typeof(DateTime?)
        };

        public static void ValidateFields(FieldInfo[] fields)
        {
            ValidateDateFields(fields);
        }

        private static void ValidateDateFields(FieldInfo[] fields)
        {
            var invalidFieldsName = fields.Where(field => field.GetCustomAttribute(typeof(CsvConverterDateAttribute)) != null && !ValidDataTypes.Contains(field.FieldType)).Select(field => field.Name);

            if (invalidFieldsName.Any())
            {
                // zamist throw zapisać do zbiorczego message i wysyłać dopiero wszystkie zebrane exception
                throw new Exception($"CsvConverterDateAttribute is invalid for {string.Join(",", invalidFieldsName)}, becouse of unchandled types"); //wylistować dozwolone typy
            }
        }
    }
}
