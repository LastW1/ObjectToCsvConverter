using System.Collections.Generic;

namespace TestingProject
{
    public class NestedValuesDto
    {
        public IEnumerable<int> NestedField = new List<int> { 1, 2, 3 };
        public IEnumerable<IEnumerable<int>> DoubleNestedField = new List<List<int>> { new List<int> { 1, 2, 3 }, new List<int> { 1, 2, 3 } };
    }
}

