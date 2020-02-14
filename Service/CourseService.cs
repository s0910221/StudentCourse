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
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork uow;

        public CourseService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<IEnumerable<Course>> GetCourses()
        {
            return await GetCourseRepository().GetList().ToListAsync();
        }

        public async Task<bool> CreateCourse(Course course)
        {
            course.CourseId = Guid.NewGuid();
            await GetCourseRepository().AddAsync(course);
            return await uow.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateCourse(Course course)
        {
            GetCourseRepository().Update(course);
            return await uow.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteCourse(Guid courseId)
        {
            var repo = GetCourseRepository();
            var deleteCourse = await repo.GetFirstAsync(x => x.CourseId == courseId);
            GetCourseRepository().Delete(deleteCourse);
            return await uow.SaveChangesAsync() > 0;
        }

        private IGenericRepository<Course> GetCourseRepository()
        {
            return uow.GetGenericRepository<Course>();
        }
    }
}
