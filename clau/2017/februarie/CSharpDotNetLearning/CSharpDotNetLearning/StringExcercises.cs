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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace CSharpDotNetLearning
{
    

    /// <summary>
    /// Get string from file "temp.txt"
    /// </summary>
    public class FileImport
    {
        /// <summary>
        /// Get string from file "temp.txt"
        /// </summary>
        /// <returns>String from text</returns>
        public string GetStringFromFile()
        {
            string text = string.Empty;

            try
            {                
                using (StreamReader sr = new StreamReader(Program.pathToProject + "temp.txt"))
                {
                    // Read the stream to a string, and write the string to the console.
                    text = sr.ReadToEnd();
                }
            }
            catch
            {
                // if problem with file or string in file put "text" to null
                text = null;
            }
            return text;
        }
    }
    /// <summary>
    /// Sets first letter of a word to Upper
    /// </summary>
    public class FirstLetterOfWordUpper
    {
        /// <summary>
        /// Represents word delimiter characters
        /// </summary>
        private string[] delimitersArray = { " ", ".", ",", "!", "?" };

        /// <summary>
        /// Represents the original string to be processed
        /// </summary>
        private String text;        

        public FirstLetterOfWordUpper()
        {
            FileImport fileImport = new FileImport();
            text = fileImport.GetStringFromFile();            
        }

        /// <summary>
        /// Browsing char;
        /// </summary>
        private StringBuilder BrowseString()
        {          
            StringBuilder textStringBuilder = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                // set the curent Char(element) 
                string currentElem = this.text[i].ToString();

                // first elem does not need to be checked
                if (i > 0)
                {
                    // set the pevious element 
                    string prevElem = this.text[i-1].ToString();

                    //Based on flag set element to UPPER
                    currentElem = CheckIfElementToBeUpper(prevElem, currentElem);
                }
                // add the char to string builder(also with the Uppercase caracter) to form the new string with the Uppercase
                textStringBuilder.Append(currentElem);
            }
            return textStringBuilder;
        }

        private string CheckIfElementToBeUpper(string prevElem, string currentElem)
        {
            //check if the curent char is from the list of delimiters
            if (Array.IndexOf(delimitersArray, prevElem) >= 0)
            {
                //check if the next char is part of the list of delimiters
                if (Array.IndexOf(delimitersArray, currentElem) == -1)
                {
                    //change into stringbuilder to Upperchase chars
                    currentElem = currentElem.ToUpper();
                }
            }
            return currentElem;
        }

        /// <summary>
        /// Split by delimiters put first clar to Upper 
        /// </summary>
        /// <returns>Returns a string builder</returns>
        private StringBuilder ArrayString()
        {
            StringBuilder textStringBuilder = new StringBuilder();
            string[] stringSplitToArray = text.Split(delimitersArray, StringSplitOptions.None);
            //string currentItem = stringSplitToArray[0];
            for (int i = 0; i < stringSplitToArray.Length; i++)
			{
                //string curElem = stringSplitToArray[i];
                if (string.IsNullOrEmpty(stringSplitToArray[i]) == false)
                {
                    char[] curElem = stringSplitToArray[i].ToCharArray();
                    curElem[0] = char.ToUpper(curElem[0]);
                    stringSplitToArray[i] = new string(curElem);
                }

                int itemLength = stringSplitToArray[i].ToString().Length; 
                int currentStringLength = textStringBuilder.Length;


                if (i == stringSplitToArray.Length-1)
                {
                    textStringBuilder.Append(stringSplitToArray[i]);
                }
                else
                {
                    string itemDelimiter = text.Substring(itemLength + currentStringLength, 1);
                    textStringBuilder.Append(stringSplitToArray[i] + itemDelimiter);
                }
                
			}
            return textStringBuilder;
        }

        private StringBuilder RegexString()
        {
            StringBuilder textStringBuilder = new StringBuilder();
            textStringBuilder.Append(Regex.Replace(text, @"([.,-][a-z]|\b[a-z])", m => m.Value.ToUpper()));
            return textStringBuilder;
        }

        public void RunParseString()
        {
            Console.WriteLine("1 - Browsing version");
            Console.WriteLine("2 - Array version");
            Console.WriteLine("3 - Regex version");
            Console.Write("Select version :");
            char[] inputArray = {'1','2','3'};
            char versionInput ;

            while (true)
                {
                    versionInput = Console.ReadKey(true).KeyChar;
                    if (Array.IndexOf( inputArray, versionInput) >= 0){
                        break;
                    }
                }            

            //start Watch for measuring the time for procesing
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            if (string.IsNullOrEmpty(this.text) == false)
            {
                Console.WriteLine("The string is Processed: {0}", versionInput.ToString());
                switch (versionInput.ToString())
                {
                    case "1":
                        Console.WriteLine(BrowseString());
                        Console.WriteLine("With brows the STRING!!!");
                        break;
                    case "2":
                        Console.WriteLine(ArrayString());
                        Console.WriteLine("With ARRAY!!");
                        break;
                    case "3":
                        Console.WriteLine(RegexString());
                        Console.WriteLine("With REGEX!!!");
                        break;
                    default:
                        Console.WriteLine("No Version Selected !!!, somehow :|");
                        break;
                }                
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