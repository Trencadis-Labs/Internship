using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessApp.Library
{
    

    public static class ConsoleHelper
    {
        /// <summary>
    /// Creates a console menu, having the specified options
    /// </summary>
    /// <param name="title">The menu title</param>
    /// <param name="options">The menu options</param>
    public static void WriteMenuWithOptions(string title, string[] options)
    {
      ConsoleHelper.WriteSeparator(title);

      if ((options == null) || (options.Length == 0))
      {
        return;
      }

      int spacesBeforeCount = (options.Length >= 10) ? 1 : 0;
      string spacesBefore = new string(' ', spacesBeforeCount);

      for (int i = 0; i < options.Length; i++)
      {
          Console.WriteLine("{0}{1}) {2}", spacesBefore, i + 1, options[i]);
      }

      ConsoleHelper.WriteSeparator();
    }

        /// <summary>
        /// Writes a separator ("-") ocuppying a full screen line
        /// </summary>
        public static void WriteSeparator()
        {
            ConsoleHelper.WriteSeparator("");
        }

        /// <summary>
        /// Writes a separator with a label (label will be framed by separators).
        /// Used to create a nice "heading".
        /// </summary>
        /// <param name="label">The label to display.</param>
        public static void WriteSeparator(string label)
        {
            string separator = new string('-', Console.WindowWidth - 1);

            Console.WriteLine(separator);

            if (!string.IsNullOrWhiteSpace(label))
            {
                int spacesCount = (Console.WindowWidth - label.Length) / 2 - 1;
                Console.Write(new string(' ', spacesCount));
                Console.Write(label);
                Console.Write(new string(' ', spacesCount));

                Console.WriteLine();
                Console.Write(separator);
                Console.WriteLine();
            }
        }
    }
}
