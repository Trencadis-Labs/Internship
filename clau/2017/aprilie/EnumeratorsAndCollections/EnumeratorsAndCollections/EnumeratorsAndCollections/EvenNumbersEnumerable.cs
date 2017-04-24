using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumeratorsAndCollections
{
    class EvenNumbersEnumerable : IEnumerable<int>
    {
        private readonly IEnumerable<int> collection;

        public EvenNumbersEnumerable(IEnumerable<int> elements)
        {
            if (elements == null)
            {
                // Enumerable class is very handy for enumeration op
                elements = Enumerable.Empty<int>();
            }

            this.collection = elements;
        }


        public IEnumerator<int> GetEnumerator()
        {
            return this.GetEvenNumerEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            IEnumerator<int> collection1 = null;
            return collection1;
        }

        private IEnumerator<int> GetEvenNumerEnumerator()
        {
            foreach (var number in this.collection)
            {
                if (number % 2 == 0)
                {
                    yield return number;
                }
            }

        }
    }
}
