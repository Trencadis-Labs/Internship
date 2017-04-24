using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Scoala.Models
{
    
    

    [Table("Grades")]
    //prepare object  Class Grade of the school class
    public class Grade
    {        
        [Key]
        public int id_grade { get; set; }
        public int id_student { get; set; }        
        public int id_cours { get; set; }

        public int grade { get; set; }
        public DateTime date_of_registration { get; set; }
        public DateTime localDate = DateTime.Now;         
    }

    public class GradeCourseStudent
    {


        [Key]
        public int id_grade { get; set; }
        public int id_student { get; set; }
        public int id_cours { get; set; }

        public string studentName { get; set; }
        public string coursName { get; set; }

        public int grade { get; set; }
        public DateTime date_of_registration { get; set; }
        public DateTime localDate = DateTime.Now;

    }

}