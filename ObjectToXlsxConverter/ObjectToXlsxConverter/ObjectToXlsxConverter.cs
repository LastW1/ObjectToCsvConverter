using ObjectToXlsxConverter.Managers;
using System;
using System.Text;
using System.Text.RegularExpressions;

namespace ObjectToXlsxConverter
{
    public class ObjectToXlsxConverter
    {
        public object ObjectToConvert { get; set; }
        public string ColumnSeparator { get; set; } = ";";

        public ObjectToXlsxConverter(object objectToConvert)
        {
            ObjectToConvert = objectToConvert;
        }

        public string ConvertToString()
        {
            var objectType = ObjectToConvert.GetType();

            var stringBuilder = new StringBuilder();

            var properties = objectType.GetProperties(); //czy tu może być zwrócony null czy co najwyżej empty?
            var propertiesCount = properties.Length;

            for(var i = 0; i< propertiesCount; i++)
            {
                stringBuilder.Append($@"""{properties[i].GetValue(ObjectToConvert)}""");

                if(i != propertiesCount - 1)
                {
                    stringBuilder.Append(ColumnSeparator);
                }
            }

            return stringBuilder.ToString();
        }

        public byte[] ConvertToUtf8Byte()
        {
            return new byte[] { };
        }

        public void ConvertToXlsx(string fileName, string filePath)
        {
            fileName = FileNameManager.CheckAndGenerateProperFileName(fileName);
        }

        private void ParseObjectToString()
        {

        }
    }
}
