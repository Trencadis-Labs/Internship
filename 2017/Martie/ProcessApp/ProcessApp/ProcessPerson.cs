using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessApp
{
    public class ProcessPerson : ProcessFlow
    {
        public Person person;

        public ProcessPerson()
        {

        }

        public override void ValidationProcess(ProcessResult processResult)
        {
            person = new Person();
            if (this.person == null)
            {
                processResult.StatusFlag = false;
                processResult.ErrorSuccesCode = ErrorCodes.NullInstance.ToString();
            }
            else
            {
                processResult.StatusFlag = true;
                processResult.ErrorSuccesCode = ErrorCodes.GoodString.ToString();
            }
        }

        public override void PreProcessingProcess(ProcessResult processResult)
        {
            //Verificare la Intrare
            this.person.FirstName = ConsoleInteratctiveMenu.ConsoleRead("FirstName");
            this.person.LastName = ConsoleInteratctiveMenu.ConsoleRead("LastName");
            this.person.Age = Convert.ToInt32(ConsoleInteratctiveMenu.ConsoleRead("Age"));
            //person = new Person(firstName, lastName, age);
        }
        public override void ProcessingProcess(ProcessResult processResult)
        {


            ProcessString processStringFirstName = new ProcessString("FirstName", this.person.FirstName);
            ProcessResult resultExecutionFirstName = processStringFirstName.Run();
            resultExecutionFirstName.ErrorSuccesMessage();

            ProcessString processStringLastName = new ProcessString("LastName", this.person.LastName);
            ProcessResult resultExecutionLastName = processStringLastName.Run();
            resultExecutionLastName.ErrorSuccesMessage();

            ProcessString processStringAge = new ProcessString("Age", this.person.Age.ToString());
            ProcessResult resultExecutionAge = processStringAge.Run();
            resultExecutionAge.ErrorSuccesMessage();

            if (string.IsNullOrEmpty(this.person.FirstName) || string.IsNullOrEmpty(this.person.LastName) || string.IsNullOrEmpty(this.person.Age.ToString()))
            {
                processResult.StatusFlag = false;
                processResult.ErrorSuccesCode = ErrorCodes.TextEmpty.ToString();
            }
            else if (string.IsNullOrWhiteSpace(this.person.FirstName) || string.IsNullOrWhiteSpace(this.person.LastName) || string.IsNullOrWhiteSpace(this.person.Age.ToString()))
            {
                processResult.StatusFlag = false;
                processResult.ErrorSuccesCode = ErrorCodes.WhitespacesOnlyText.ToString();
            }
            else
            {
                processResult.StatusFlag = true;
                processResult.ErrorSuccesCode = ErrorCodes.GoodString.ToString();
            }
        }

        public override void PostProcessingProcess(ProcessResult processResult)
        {
            ConsoleInteratctiveMenu.ConsoleWrite("Person NO. ", Person.populationCount.ToString() + " " + base.PID.ToString());
        }
    }
}
