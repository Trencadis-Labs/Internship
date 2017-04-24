using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Scoala.Models
{
    //prepare object  Class Course of the school class
    [Table("Courses")]
    public class Course
    {
        [Key]
        public int id_cours { get; set; }
        public string name { get; set; }
        public DateTime date_of_registration { get; set; }
        public DateTime localDate = DateTime.Now;
    }



}