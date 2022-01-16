using ObjectToCsvConverter.Attributes;
using ObjectToCsvConverter.Managers;
using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Text;

namespace ObjectToCsvConverter
{
    public class ObjectToCsvConverter
    {
        public object ObjectToConvert { get; set; }
        public string ColumnSeparator { get; set; } = ";";
        public string NestedCollectionSeparetor { get; set; } = ",";
        public string CollectionItemsSeparator { get; set; } = "\r\n";
        public bool IsNullValueOverridedWithString = false;
        public string NullOverridingValue = "NULL";

        public ObjectToCsvConverter(object objectToConvert)
        {
            ObjectToConvert = objectToConvert;
        }

        public string ConvertToString()
        {
            var objectType = ObjectToConvert.GetType();

            var stringBuilder = new StringBuilder();

            if (ReflectionManager.IsTypeIEnumerable(objectType))
            {
                ConvertFromCollection(stringBuilder);
            }
            else
            {
                ConvertFromSingleInstance(stringBuilder);
            }

            return stringBuilder.ToString();
        }

        private void ConvertFromCollection(StringBuilder stringBuilder)
        {
            var collectionObject = (IEnumerable)ObjectToConvert;

            FieldInfo[] fields = null;

            foreach (var singleObject in collectionObject)
            {
                if (fields == null)
                {
                    var singleType = singleObject.GetType();
                    fields = singleType.GetFields();
                }

                WritePropertiesToString(stringBuilder, fields, singleObject);
                stringBuilder.Append(CollectionItemsSeparator);
            }
        }

        private void ConvertFromSingleInstance(StringBuilder stringBuilder)
        {
            var objectType = ObjectToConvert.GetType();
            var fields = objectType.GetFields();
            WritePropertiesToString(stringBuilder, fields, ObjectToConvert);
        }

        private void WritePropertiesToString(StringBuilder stringBuilder, FieldInfo[] fields, object objectToParse)
        {
            AttributeValidatorManager.ValidateFields(fields);

            var fieldsCount = fields.Length;

            for (var i = 0; i < fieldsCount; i++)
            {
                var fieldValue = fields[i].GetValue(objectToParse);
                var fieldAttribute = fields[i].GetCustomAttribute(typeof(CsvConverterDateAttribute));
                if (IsNullValueOverridedWithString && fieldValue == null)
                {
                    stringBuilder.Append(NullOverridingValue);
                }
                else
                {
                    var valueType = fieldValue.GetType();

                    if (ReflectionManager.IsTypeIEnumerable(valueType))
                    {
                        stringBuilder.Append($"\t{string.Join(NestedCollectionSeparetor, TypesManager.UnknownEnumerableToStringEnumerable((IEnumerable)fieldValue))}");
                    }
                    else
                    {
                        FitToAttributes(stringBuilder, fieldValue, fieldAttribute);
                       // stringBuilder.Append($"\t{fieldValue}");
                    }
                }

                if (i != fieldsCount - 1)
                {
                    stringBuilder.Append(ColumnSeparator);
                }
            }
        }

        private void FitToAttributes(StringBuilder stringBuilder, object fieldValue, Attribute attribute)
        {
            if (attribute == null || fieldValue == null)
            {
                stringBuilder.Append($"\t{fieldValue}");
            }
            else
            {
                // stringBuilder.Append($"{(DateTime)fieldValue:yyyy-MM-dd HH:mm:ss}");
                stringBuilder.Append($"{(DateTime)fieldValue:yyyy-MM-dd HH:mm:ss}");
               // stringBuilder.Append($"{(DateTime)fieldValue}");
            }
        }

        public void ConvertToCsv(string fileName, string filePath = null)
        {
            var stringData = ConvertToString();

            fileName = FileNameManager.CheckAndGenerateProperFileName(fileName);

            using (StreamWriter writetext = new StreamWriter(filePath == null ? fileName : $"{filePath}\\{fileName}"))
            {
                writetext.Write(stringData);
            }
        }
    }
}
