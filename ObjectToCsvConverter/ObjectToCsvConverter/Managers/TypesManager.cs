using System.Collections;
using System.Collections.Generic;

namespace ObjectToCsvConverter.Managers
{
    public static class TypesManager
    {
        public static IEnumerable<string> UnknownEnumerableToStringEnumerable(IEnumerable enumerable)
        {
            var resultList = new List<string>();

            foreach (var singleValue in enumerable)
            {
                resultList.Add(singleValue.ToString());
            }

            return resultList;
        }
    }
}
