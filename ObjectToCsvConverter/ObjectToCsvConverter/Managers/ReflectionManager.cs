using System;
using System.Collections;

namespace ObjectToCsvConverter.Managers
{
    public static class ReflectionManager
    {
        public static bool IsTypeIEnumerable(Type type)
        {
            return type.GetInterface(nameof(IEnumerable)) != null;
        }
    }
}
