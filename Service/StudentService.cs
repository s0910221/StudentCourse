using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entity.Data;
using Entity.Entity;
using Microsoft.EntityFrameworkCore;
using Service.Interface;

namespace Service
{
    public class StudentService : IStudentService
    {
        private readonly StudentCourseContext db;

        public StudentService(StudentCourseContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<Student>> GetStudents()
        {
            return await db.Students.ToListAsync();
        }

        public async Task<bool> CreateStudent(Student student)
        {
            student.StudentId = Guid.NewGuid();
            await db.AddAsync(student);
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateStudent(Student student)
        {
            db.Entry(student).State = EntityState.Modified;
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteStudent(Guid studentId)
        {
            var deleteStudent = await db.Students.FindAsync(studentId);
            db.Students.Remove(deleteStudent);
            return await db.SaveChangesAsync() > 0;
        }

    }
}
