using System;
using System.Collections.Generic;

namespace CodeGenerated.Models
{
    public partial class Student
    {
        public int stuID { get; set; }
        public string stuName { get; set; }
        public int deptID { get; set; }
        public virtual Department Department { get; set; }
    }
}
