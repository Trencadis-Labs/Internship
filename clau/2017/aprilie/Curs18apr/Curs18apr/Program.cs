using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Diagnostics;

namespace Curs18apr
{
    class Program
    {
        private static void WriteExceptionDetails(Exception e)
        {
            Console.WriteLine("Error message = " + e.Message);
            Console.WriteLine("StackTrace message = " + e.StackTrace);
            Console.WriteLine("Exception type = " + e.GetType());

            if (e.InnerException != null)
            {
                WriteExceptionDetails(e.InnerException);
            }
        }

        private static DataTable GetData()
        {
            // data-table structure
            DataTable data = new DataTable();
            data.Columns.AddRange(new[] {
                new DataColumn("Value", typeof(int))
            });

            // simulate a DB read
            for (int i = 0; i < 10000; i++)
            {
                if (i % 3 == 0)
                {
                    data.Rows.Add(new object[] { DBNull.Value });
                }
                else
                {
                    data.Rows.Add(i);
                }
            }

            return data;
        }

        private static void ErrorHandlingStopWatch()
        {
            var data = GetData();

            // effective code:
            var watch1 = Stopwatch.StartNew();
            List<int> convertResult1 = new List<int>();
            foreach (DataRow row in data.Rows)
            {
                int converted = IntegerConverter.ConvertToInt(
                                    row["Value"],
                                    -1);
                if (converted != -1)
                {
                    convertResult1.Add(converted);
                }
            }
            watch1.Stop();

            Console.WriteLine(
               "Conversion 1 finished in {0} ms",
               watch1.ElapsedMilliseconds);



            var watch2 = Stopwatch.StartNew();
            List<int> convertResult2 = new List<int>();
            foreach (DataRow row in data.Rows)
            {
                int converted = IntegerConverter.ConvertToInt2(
                                    row["Value"],
                                    -1);
                if (converted != -1)
                {
                    convertResult2.Add(converted);
                }
            }
            watch2.Stop();

            Console.WriteLine(
                "Conversion 2 finished in {0} ms",
                watch2.ElapsedMilliseconds);
        }

        static void Main(string[] args)
        {

            //Program.ErrorHandlingStopWatch();


            /* var intSorter = new ArraySorter<int>(2, 5, 3, 1, 4);

             var stringSorter = new ArraySorter<string>("one", "two", "treee");

             var personSorter = new ArraySorter<Person>(
                 new Person
                 {
                     FirstName = "Joe",
                     LastName = "Doe"
                 });

             var sortedString = stringSorter.SortAscending(); */

            var cofeeMachine = new CofeeMaker();
            Console.WriteLine(cofeeMachine.CurrentState);

            cofeeMachine.HandleAction(CofeeMakerActions.InsertCoin);
            Console.WriteLine(cofeeMachine.CurrentState);

            cofeeMachine.HandleAction(CofeeMakerActions.SelectBeverage);
            Console.WriteLine(cofeeMachine.CurrentState);

            cofeeMachine.HandleAction(CofeeMakerActions.BeveragePickedUp);
            Console.WriteLine(cofeeMachine.CurrentState);


            Console.ReadKey();
        }
    }
}
