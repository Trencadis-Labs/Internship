using System;
using System.Diagnostics;
using System.ComponentModel;

namespace ProcessApp
{


    /// <summary>
    /// Run process Core
    /// </summary>
    /// 
    public class ProcessFlow
    {
        enum errorCode1 :string
        {
            textEmpty = "ERROR: Text is empty",
            noVisibleText = "ERROR: Text has no visible text"

        }



        public readonly Guid PID;

        public bool statusFlag { get; set; }

        protected string errorCode { get; set; }

        protected string SuccesfulCode { get; set; }


        /// <summary>
        /// initialize values in CTOR
        /// </summary>
        public ProcessFlow()
        {
            statusFlag = true;
            errorCode = string.Empty;
            SuccesfulCode = string.Empty;
            PID = System.Guid.NewGuid();
        }

        public string ConsoleInput(string inputLabel)
        {
            Console.Write("Input data:{0} = ", inputLabel);
            string inputConsole = Console.ReadLine();
            return inputConsole;
        }
        public bool VerifyInputValue(string inputConsole, string type)
        {
            if (string.IsNullOrEmpty(inputConsole))
            {
                statusFlag = false;
                this.errorCode = "ERROR: Text is empty";
            }
            else if (string.IsNullOrWhiteSpace(inputConsole))
            {
                statusFlag = false;
                this.errorCode = "ERROR: Text has no visible text";
            }
            else if (type == "int")
            {
                int num;
                if (int.TryParse(inputConsole, out num) != true)
                {
                    statusFlag = false;
                }
            }
            else
            {
                statusFlag = true;
                this.SuccesfulCode = "PASSED: Text is good for processing";
            }
            return statusFlag;
        }

        protected void DisplayOutputProcessdata(string text)
        {
            Console.WriteLine(text.ToString());
        }

        protected void ErrorHandling(string errorCodeHandling)
        {
            if (this.statusFlag == false)
            {
                Console.WriteLine(errorCodeHandling.ToString() + ", PID:{0}", this.PID);
            }
        }
        protected void SuccessHandling(string SuccesfulCodeHandling)
        {
            if (this.statusFlag == true)
            {
                Console.WriteLine(SuccesfulCodeHandling.ToString() + ", PID:{0}", this.PID);
            }
        }

        // This could be removed and rethink ConsoleInput and VerifyInputValue
        private string ConsoleInputAndVerifyInputValue(string fieldName, string type)
        {
            string inputedData = string.Empty;
            inputedData = ConsoleInput(fieldName);
            this.statusFlag = VerifyInputValue(inputedData, type);
            return inputedData;
        }
        public void Run(string stringProperty)
        {
            string inputedData = string.Empty;
            inputedData = ConsoleInputAndVerifyInputValue("String", "string");


            if (this.statusFlag == true)
            {
                this.SuccessHandling(this.SuccesfulCode);
                this.DisplayOutputProcessdata(inputedData);
            }
            else
            {
                this.ErrorHandling(this.errorCode);
            }
        }


        // can be used Run(string)
        public void Run(Person person)
        {
            if (person != null)
            {
                // can be made more object oriented
                string inputedData = string.Empty;
                inputedData = this.ConsoleInputAndVerifyInputValue("FirstName", "string");
                if (this.statusFlag == true)
                {
                    person["FirstName"] = inputedData;
                }

                inputedData = string.Empty;
                inputedData = this.ConsoleInputAndVerifyInputValue("LastName", "string");
                if (this.statusFlag == true)
                {
                    person["LastName"] = inputedData;
                }

                inputedData = string.Empty;
                inputedData = this.ConsoleInputAndVerifyInputValue("Age", "int");
                if (this.statusFlag == true)
                {
                    person["Age"] = inputedData;
                }
            }
            else
            {
                this.ErrorHandling("ERROR: Instance missing !!!");
            }

            Console.WriteLine(Person.populationCount);
        }
    }
}
