using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project_8.Models
{
    public class StudentClassViewModel
    {
        public Student_Class StudentClass { get; set; }
        public List<AspNetUser> Users { get; set; }
    }
}