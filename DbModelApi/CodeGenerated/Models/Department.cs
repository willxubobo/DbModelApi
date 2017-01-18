using System;
using System.Collections.Generic;

namespace CodeGenerated.Models
{
    public partial class Department
    {
        public Department()
        {
            this.Students = new List<Student>();
        }

        public int depID { get; set; }
        public string depName { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
