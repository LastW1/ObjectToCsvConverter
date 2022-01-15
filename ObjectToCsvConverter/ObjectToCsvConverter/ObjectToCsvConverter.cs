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

        public ObjectToCsvConverter(object objectToConvert)
        {
            ObjectToConvert = objectToConvert;
        }

        public string ConvertToString()
        {
            var objectType = ObjectToConvert.GetType();

            var stringBuilder = new StringBuilder();

            var isCollection = objectType.GetInterface(nameof(ICollection)) != null;

            if (isCollection)
            {
                var collectionObject = (ICollection)ObjectToConvert;

                PropertyInfo[] properties = null;

                foreach (var singleObject in collectionObject)
                {
                    if (properties == null)
                    {
                        var singleType = singleObject.GetType();
                        properties = singleType.GetProperties();
                    }

                    WritePropertiesToString(stringBuilder, properties, singleObject);
                    stringBuilder.Append(CollectionItemsSeparator);
                }
            }
            else
            {
                var properties = objectType.GetProperties();
                WritePropertiesToString(stringBuilder, properties, ObjectToConvert);
            }

            return stringBuilder.ToString();
        }

        private void WritePropertiesToString(StringBuilder stringBuilder, PropertyInfo[] properties, object objectToParse)
        {
            var propertiesCount = properties.Length;

            for (var i = 0; i < propertiesCount; i++)
            {
                stringBuilder.Append($@"""{properties[i].GetValue(objectToParse)}""");

                if (i != propertiesCount - 1)
                {
                    stringBuilder.Append(ColumnSeparator);
                }
            }
        }

        public byte[] ConvertToUtf8Byte()
        {
            return new byte[] { };
        }

        public void ConvertToXlsx(string fileName, string filePath = null)
        {
            var stringData  = ConvertToString();
            fileName = FileNameManager.CheckAndGenerateProperFileName(fileName);

            using (StreamWriter writetext = new StreamWriter(fileName))
            {
                writetext.Write(stringData);
            }
        }

        private void ParseObjectToString()
        {

        }
    }
}
