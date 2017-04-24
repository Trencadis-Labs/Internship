using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curs18apr
{
    public static class IntegerValuesProcessing
    {
        public static void DoSomeProcessing()
        {
            var strings = new[] { "1", "2", null, "Abc" };
            List<int> convertedValues = new List<int>();

            foreach (var s in strings)
            {
                try
                {
                    int converted = IntegerConverter.ConvertToInt(s);
                    convertedValues.Add(converted);
                }
                catch
                {
                    Console.WriteLine("Error at string conversion: {0}", s);
                    throw;
                    //throw new ApplicationException("Eroare la conversie!");
                }
            }

            foreach (int i in convertedValues)
            {
                Console.WriteLine(i);
            }
        }
    }
}
