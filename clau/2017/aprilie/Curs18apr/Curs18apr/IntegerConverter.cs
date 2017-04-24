using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curs18apr
{
    public static class IntegerConverter
    {
        public static int ConvertToInt(object value, int defaultValue = 0)
        {

            try
            {
                int intValue = Convert.ToInt32(value);
                return intValue;
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
                //throw new ApplicationException("Eroare la Conversie");
                return defaultValue;
            }
        }

        public static int ConvertToInt2(object value, int defaultValue = 0)
        {
            if ((value == null) || (value == DBNull.Value))
            {
                return defaultValue;
            }
            try
            {
                int intValue = Convert.ToInt32(value);
                return intValue;
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
                //throw new ApplicationException("Eroare la Conversie");
                return defaultValue;
            }
        }
    }
}
