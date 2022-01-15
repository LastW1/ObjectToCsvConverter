using ObjectToCsvConverter.Managers;
using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace ObjectToCsvConverter
{
    public class ObjectToCsvConverter
    {
        public object ObjectToConvert { get; set; }
        public string ColumnSeparator { get; set; } = ";";
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
                    //  stringBuilder.Append($@"""{fields[i].GetValue(objectToParse)}""");
                    //   stringBuilder.Append($"\"=\"\"{fields[i].GetValue(objectToParse)}\"\"\"");
                    stringBuilder.Append($"\t{fields[i].GetValue(objectToParse)}"); // tab prevents from automatic parsing values like dates
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

        private void ParseObjectToString()
        {

        }
    }
}
