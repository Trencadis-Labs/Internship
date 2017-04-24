using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayManagement
{


    public class ArrayHelper<T>
        where T : IEquatable<T>
    {
        protected readonly T[] elements;

        public ArrayHelper(params T[] arrays)
        {
            if (elements == null)
            {
                elements = new T[] { };
            }

            this.elements = arrays;
        }

        /// <summary>
        /// Get row length of an array of type T
        /// </summary>
        /// <param name="array"></param>
        /// <returns>Row Length</returns>
        public int GetRowLength()
        {
            int rowLength = 0;
            rowLength = this.elements.Length;
            return rowLength;
        }


        /// <summary>
        /// Return index of a given element, from an array
        /// </summary>
        /// <param name="element"></param>
        /// <returns>Index<int></returns>
        public int SearchIndexOf(T element)
        {
            int index = -1;
            for (int i = 0; i < this.elements.Length; i++)
            {
                if (this.elements[i].Equals(element))
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        /// <summary>
        /// Gel element of an aray by an index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T GetElemByIndex(int index)
        {
            T elem;
            try
            {
                elem = this.elements[index];
            }
            catch (IndexOutOfRangeException e)
            {
                elem = default(T);
                Console.WriteLine(e.Message);
            }

            return elem;
        }

        public void printArrayToConsole()
        {
            for (int i = 0; i < this.elements.Length; i++)
            {
                Console.WriteLine(this.elements[i]);
            }
        }

        public List<T> SubstringReturn(int start, int length)
        {
            T[] substring;
            List<T> iList = new List<T>();
            for (int i = start; i <= start + length; i++)
            {
                iList.Add(this.elements[i]);
            }
            return iList;
        }
    }
}
