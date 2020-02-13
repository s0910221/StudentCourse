using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Entity
{
    public class Course
    {
        public Guid CourseId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
