using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curs18apr
{
    public class ArraySorter<T>
        where T : IComparable<T>
    {

        private T[] elements;

        public ArraySorter(params T[] elements)
        {
            this.elements = elements;
        }

        public T[] SortAscending()
        {

            //var result = new T[elements.Length];

            for (int i = 0; i < this.elements.Length; i++)
            {
                for (int j = i + 1; j < this.elements.Length; j++)
                {
                    if (this.elements[i].CompareTo(this.elements[j]) > 0)
                    {
                        // this means: elements[i] > elements[j]
                        // we swap:
                        var aux = this.elements[i];
                        this.elements[i] = this.elements[j];
                        this.elements[j] = aux;
                    }
                }
            }

            return elements;

        }

    }
}
