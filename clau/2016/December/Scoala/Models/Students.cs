using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Scoala.Models
{
    // used for convertig column sex(int) to string
    public enum Sexo { Male, Female };
    //prepare object  students of the school class
    [Table("Students")]
    public class Student
    {   
        [Key]
        public int id_student { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public Sexo sex { get; set; }

        public string FullName
        {
            get
            {
                return String.Format("{0} {1}", name, surname);
            }
        }

        public DateTime date_of_registration { get; set; }
        public DateTime localDate  = DateTime.Now ;
        
    }


}