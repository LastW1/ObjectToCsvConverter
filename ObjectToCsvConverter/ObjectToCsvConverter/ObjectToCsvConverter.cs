using ObjectToCsvConverter.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

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

            var isEnumerable = objectType.GetInterface(nameof(IEnumerable)) != null;

            if (isEnumerable)
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
            else
            {
                var fields = objectType.GetFields();
                WritePropertiesToString(stringBuilder, fields, ObjectToConvert);
            }

            return stringBuilder.ToString();
        }

        private void WritePropertiesToString(StringBuilder stringBuilder, FieldInfo[] fields, object objectToParse)
        {
            var fieldsCount = fields.Length;

            for (var i = 0; i < fieldsCount; i++)
            {
                var fieldValue = fields[i].GetValue(objectToParse);

                if(fieldValue == null)
                {
                    stringBuilder.Append(NullOverridingValue);
                }
                else
                {
                    var valueType = fieldValue.GetType();

                    if (valueType.GetInterface(nameof(IEnumerable)) != null)
                    {
                        stringBuilder.Append($"\t{string.Join(NestedCollectionSeparetor, UnknownEnumerableToStringEnumerable((IEnumerable)fieldValue))}");
                    }
                    else
                    {
                        stringBuilder.Append($"\t{fieldValue}"); // tab prevents from automatic parsing values like dates
                    }
                    //  stringBuilder.Append($@"""{fields[i].GetValue(objectToParse)}""");
                    //   stringBuilder.Append($"\"=\"\"{fields[i].GetValue(objectToParse)}\"\"\"");
                }

                if (i != fieldsCount - 1)
                {
                    stringBuilder.Append(ColumnSeparator);
                }
            }
        }

        public void ConvertToCsv(string fileName, string filePath = null)
        {
            var stringData  = ConvertToString();

            fileName = FileNameManager.CheckAndGenerateProperFileName(fileName);

            using (StreamWriter writetext = new StreamWriter(filePath == null ? fileName : $"{filePath}\\{fileName}"))
            {
                writetext.Write(stringData);
            }
        }

        private IEnumerable<string> UnknownEnumerableToStringEnumerable(IEnumerable enumerable)
        {
            var resultList = new List<string>();

            foreach(var singleValue in enumerable)
            {
                resultList.Add(singleValue.ToString());
            }

            return resultList;
        }
    }
}
