using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumeratorsAndCollections
{
    class Program
    {
        static void Main(string[] args)
        {

            // interface IEnumerable<int>

            EvenNumbersEnumerable e = new EvenNumbersEnumerable(new EnumerableObject(0, 1, 2, 3, 4, 5, 6, 7, 8, 9));

            Console.WriteLine(e.Count());
            foreach (int element in e)
            {
                Console.Write(element + ", ");
            }

            Console.ReadKey();
        }
    }
}
