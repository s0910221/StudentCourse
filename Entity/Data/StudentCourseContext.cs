using System;
using System.Collections.Generic;
using System.Text;
using Entity.Entity;
using Microsoft.EntityFrameworkCore;

namespace Entity.Data
{
    public partial class StudentCourseContext : DbContext
    {

        public StudentCourseContext()
        {
        }

        public StudentCourseContext(DbContextOptions<StudentCourseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourse>(entity =>
            {
                entity.HasKey(e => new { e.StudentId, e.CourseId });
            });
        }

    }
}
