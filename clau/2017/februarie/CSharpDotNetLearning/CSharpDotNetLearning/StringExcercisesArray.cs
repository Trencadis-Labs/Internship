//    ============CERINTE============
// Dandu-se un text, transformati fiecare prima litera (initialele) a unui cuvant intr-o litera mare; Cuvintele se vor considera 
// ca fiind separate de caracterul spatiu, insa va trebui sa tratati si cazul in care din greseala, textul contine si mai multe spatii, 
// sau caractere whitespace (tab, enter, etc). De asemenea, textul poate contine si semne de punctuatie, care nu trebuie sa fie socotite 
// drept cuvinte, va trebui sa oferiti suport pentru urmatoarele semne de punctuatie: “.” si “,”.

// OBS :  1/ the use of static (class, fields, methods), if changes state of object(when fiels or atributes, proprieties) 
//          use instances, think if it is used with more threads.
//        2/ encapsulation (put the acces modifiers specificaly)
//        3/ make smaller classes and reduce the methods in it.
//        4/ currentElem use it better as a method parameter (easyer to be read by others).
// DONE WITH ARRAYS
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.IO;

namespace CSharpDotNetLearning
{   
    /// <summary>
    /// Sets first letter of a word to Upper
    /// </summary>
    public class FirstLetterOfWordUpperArray
    {
        /// <summary>
        /// Represents word delimiter characters
        /// </summary>
        private string[] delimitersArray = { " ", ".", ",", "!", "?" };

        /// <summary>
        /// Represents the original string to be processed
        /// </summary>
        private String text;

        public FirstLetterOfWordUpperArray()
        {
            FileImport fileImport = new FileImport();
            text = fileImport.GetStringFromFile();            
        }

        private StringBuilder BrowseString()
        {
            StringBuilder textStringBuilder = new StringBuilder();



            return textStringBuilder;
        }
        
        public void RunParseString()
        {
            //start Watch for measuring the time for procesing
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            if (string.IsNullOrEmpty(this.text) == false)
            {
                Console.WriteLine("The string is Processed");
                Console.WriteLine(BrowseString());
            }
            else
            {
                Console.WriteLine("The string is empty or null!!!");
            }

            Console.WriteLine("=================================================================");
            Console.WriteLine("Read file path: {0}temp.txt",Program.pathToProject);
            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
        }
    }    
}