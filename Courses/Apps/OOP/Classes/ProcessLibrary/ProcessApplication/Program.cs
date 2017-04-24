using ProcessApplication.Processes;
using ProcessLibrary;
using System;

namespace ProcessApplication
{
  class Program
  {
    private static void WriteProcessDetails(Process p)
    {
      if (p == null)
      {
        return;
      }

      Console.WriteLine("{0} - PID = {1}", p.GetType(), p.PID);
    }

    private static void WriteProcessResult(ProcessResult result)
    {
      if(result == null)
      {
        Console.WriteLine("Process result: (null)");
        return;
      }

      Console.WriteLine("Process result: success = {0}, errorCode = {1}", result.WasSuccessfull, result.ErrorCode);
    }

    private static void WriteSeparator()
    {
      string separator = new string('-', Console.WindowWidth - 1);

      Console.WriteLine(separator);
    }

    static void Main(string[] args)
    {
      var stringProcess = new WriteStringToConsoleProcess("Test write string to console process");
      WriteProcessDetails(stringProcess);
      var resultString = stringProcess.Run();
      WriteProcessResult(resultString);

      WriteSeparator();

      var person = new Person
      {
        FirstName = "John",
        LastName = "Doe",
        Age = 25
      };

      var personProcess = new WritePersonPropertiesToConsoleProcess(person);
      WriteProcessDetails(personProcess);
      var resultPerson = personProcess.Run();
      WriteProcessResult(resultPerson);

      WriteSeparator();

      var personWithErrorProcess = new WritePersonPropertiesToConsoleWithExceptionProcess(person);
      WriteProcessDetails(personWithErrorProcess);
      var resultPersonWithError = personWithErrorProcess.Run();
      WriteProcessResult(resultPersonWithError);

      Console.WriteLine("Press any key to close ...");
      Console.ReadKey();
    }
  }
}
