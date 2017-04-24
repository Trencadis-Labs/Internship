using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumeratorsAndCollections
{
    class EnumerableObject : IEnumerable<int>
    {
        private readonly IEnumerable<int> collection;

        public EnumerableObject(params int[] elements)
        {
            if (elements == null)
            {
                elements = new int[] { };
            }
            this.collection = elements;
        }

        public IEnumerator<int> GetEnumerator()
        {
            return collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return collection.GetEnumerator();
        }

        private IEnumerator<int> GetElementsEnumerator()
        {
            foreach (int element in this.collection)
            {
                yield return element;
            }

        }
    }
}
