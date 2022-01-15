using System;
using System.Text.RegularExpressions;

namespace ObjectToCsvConverter.Managers
{
    public static class FileNameManager // to jest bardziej helper niż manager
    {
        public static string CheckAndGenerateProperFileName(string fileName)
        {
            // dodać sprawdzanie czy nazwa nie zawiera spacji i innych niedozwolonych znaków

            var fileExtentionRegex = new Regex(@"\.[a-zA-Z]+$");
            if (fileExtentionRegex.IsMatch(fileName))
            {
                if (!fileName.ToLower().EndsWith(".csv"))
                {
                    throw new Exception("Incorrect file name extention passed!"); //zrobić custom exception
                }

            }
            else
            {
                fileName += ".csv";
            }

            return fileName;
        }
    }
}

