using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entity.Entity;

namespace Service.Interface
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetStudents();
        Task<bool> CreateStudent(Student student);
        Task<bool> UpdateStudent(Student student);
        Task<bool> DeleteStudent(Guid studentId);
    }
}
