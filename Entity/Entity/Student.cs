using System;
using System.Collections.Generic;
using System.Text;
using Entity.Entity.Enums;

namespace Entity.Entity
{
    public class Student
    {
        public Guid StudentId { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; }
        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
    }

}
