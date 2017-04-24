using ProcessLibrary;
using System;

namespace ProcessApplication.Processes
{
  /// <summary>
  /// Represents the process which writes a person's properties to console
  /// </summary>
  public class WritePersonPropertiesToConsoleProcess : Process
  {
    /// <summary>
    /// Holds the number of process instances
    /// </summary>
    private static int InstancesCount = 0;

    /// <summary>
    /// Initializez a new instance of the <see cref="WritePersonPropertiesToConsoleProcess"/> class
    /// </summary>
    /// <param name="person">The person instance</param>
    public WritePersonPropertiesToConsoleProcess(Person person)
    {
      this.Person = person;
    }

    /// <summary>
    /// Gets the process identifier
    /// </summary>
    public override int PID
    {
      get
      {
        return (int)ProcessIds.WritePersonPropertiesToConsole;
      }
    }

    /// <summary>
    /// Gets the person instance (who's properties will be written to console)
    /// </summary>
    public Person Person
    {
      get;
      private set;
    }

    /// <summary>
    /// Validates the person instance
    /// </summary>
    /// <returns>The process validation result</returns>
    protected override ProcessResult Validate()
    {
      if(this.Person == null)
      {
        return ProcessResult.Fail(ApplicationErrorCodes.WritePersonPropertiesToConsole.PersonInstanceIsNull);
      }

      return ProcessResult.Success();
    }

    /// <summary>
    /// On processing phase, the process effectively writes the person's properties to console
    /// </summary>
    /// <returns>The processing result</returns>
    protected override ProcessResult OnProcessing()
    {
      var writeFirstNameProcess = new WriteStringToConsoleProcess(this.Person.FirstName);
      var firstNameResult = writeFirstNameProcess.Run();
      if (!firstNameResult.WasSuccessfull)
      {
        return firstNameResult;
      }

      var writeLastNameProcess = new WriteStringToConsoleProcess(this.Person.LastName);
      var lastNameResult = writeLastNameProcess.Run();
      if (!lastNameResult.WasSuccessfull)
      {
        return lastNameResult;
      }

      var writeAgeProcess = new WriteStringToConsoleProcess(this.Person.Age.ToString());
      var ageResult = writeAgeProcess.Run();
      if (!ageResult.WasSuccessfull)
      {
        return ageResult;
      }

      return ProcessResult.Success();
    }

    /// <summary>
    /// On post-processing phase the process reports the number of person instances written till now
    /// </summary>
    /// <returns>The post-processing result (success)</returns>
    protected override ProcessResult OnPostProcessing()
    {
      WritePersonPropertiesToConsoleProcess.InstancesCount++;

      Console.WriteLine("Person(s) written till now: {0}", WritePersonPropertiesToConsoleProcess.InstancesCount);

      return ProcessResult.Success();
    }
  }
}
