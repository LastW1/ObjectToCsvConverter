using ObjectToCsvConverter.Managers;
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
            var fields = objectType.GetFields();
            WritePropertiesToString(stringBuilder, fields, ObjectToConvert);
        }

        private void WritePropertiesToString(StringBuilder stringBuilder, FieldInfo[] fields, object objectToParse)
        {
            var fieldsCount = fields.Length;

            for (var i = 0; i < fieldsCount; i++)
            {
                var fieldValue = fields[i].GetValue(objectToParse);
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
                        stringBuilder.Append($"\t{fieldValue}");
                    }
                }

                if (i != fieldsCount - 1)
                {
                    stringBuilder.Append(ColumnSeparator);
                }
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
