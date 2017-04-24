using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curs18apr
{
    public class Person
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public Person(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public int CompareTo(Person other)
        {
            string fullName = this.FirstName + " " + this.LastName;

            string otherFullName = string.Empty;

            if (other != null)
            {
                otherFullName = other.FirstName + " " + other.LastName;
            }

            return fullName.CompareTo(otherFullName);
        }
    }
}
