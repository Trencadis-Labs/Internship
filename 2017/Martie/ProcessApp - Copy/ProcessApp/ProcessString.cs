using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessApp
{
    public class ProcessString
    {
        /// <summary>
        /// Run the process for string values 
        /// </summary>
        /// <param name="textString">Input string values</param>
        public void Run(string stringProperty, int i)
        {

            string inputedData = string.Empty;
            bool succesFlag = false;
            inputedData = base.ConsoleInput("String");
            succesFlag = base.VerifyInputDataString(inputedData);

            if (base.VerifyInputDataString(inputedData) == true)
            {
                base.SuccessHandling(SuccesfulCode);
                DisplayOutputProcessdata(inputedData);
            }
            else
            {
                ErrorHandling(this.errorCode);
            }

        }
    }
}
