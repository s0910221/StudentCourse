using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entity.Entity;

namespace Service.Interface
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetCourses();
        Task<bool> CreateCourse(Course course);
        Task<bool> UpdateCourse(Course course);
        Task<bool> DeleteCourse(Guid courseId);
    }
}
