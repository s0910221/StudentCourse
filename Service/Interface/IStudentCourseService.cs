using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entity.Entity;

namespace Service.Interface
{
    public interface IStudentCourseService
    {
        Task<IEnumerable<StudentCourse>> GetStudentCourseByStudentId(Guid studentId);
        Task<bool> UpsertStudentCourseByStudent(Guid studentId, IEnumerable<Guid> courseIds);
        Task<IEnumerable<StudentCourse>> GetStudentCourseByCourseId(Guid courseId);
        Task<bool> UpsertStudentCourseByCourse(Guid courseId, IEnumerable<Guid> studentIds);
    }
}
