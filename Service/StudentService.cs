using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entity.Data;
using Entity.Entity;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using Service.Interface;

namespace Service
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork uow;

        public StudentService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<IEnumerable<Student>> GetStudents()
        {
            return await GetStudentRepository().GetList().ToListAsync();
        }

        public async Task<bool> CreateStudent(Student student)
        {
            student.StudentId = Guid.NewGuid();
            await GetStudentRepository().AddAsync(student);
            return await uow.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateStudent(Student student)
        {
            GetStudentRepository().Update(student);
            return await uow.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteStudent(Guid studentId)
        {
            var repo = GetStudentRepository();
            var deleteStudent = await repo.GetFirstAsync(x => x.StudentId == studentId);
            GetStudentRepository().Delete(deleteStudent);
            return await uow.SaveChangesAsync() > 0;
        }

        private IGenericRepository<Student> GetStudentRepository()
        {
            return uow.GetGenericRepository<Student>();
        }
    }
}
