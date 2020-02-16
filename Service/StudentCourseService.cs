using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Data;
using Entity.Entity;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using Service.Interface;

namespace Service
{
    public class StudentCourseService : IStudentCourseService
    {
        private readonly IUnitOfWork uow;

        public StudentCourseService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<IEnumerable<StudentCourse>> GetStudentCourseByStudentId(Guid studentId)
        {
            return await GetStudentCourseRepository().GetList(x => x.StudentId == studentId).ToListAsync();
        }

        public async Task<bool> UpsertStudentCourseByStudent(Guid studentId, IEnumerable<Guid> courseIds)
        {
            var repo = GetStudentCourseRepository();
            var dbData = await GetStudentCourseByStudentId(studentId);
            var addData = courseIds.Where(x => !dbData.Any(y => y.CourseId == x))
                .Select(x => new StudentCourse()
                {
                    StudentId = studentId,
                    CourseId = x
                }).ToList();
            var deleteData = dbData.Where(x => !courseIds.Any(y => y == x.CourseId));
            await repo.AddAsync(addData);
            repo.Delete(deleteData);
            return await uow.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<StudentCourse>> GetStudentCourseByCourseId(Guid courseId)
        {
            return await GetStudentCourseRepository().GetList(x => x.CourseId == courseId).ToListAsync();
        }

        public async Task<bool> UpsertStudentCourseByCourse(Guid courseId, IEnumerable<Guid> studentIds)
        {
            var repo = GetStudentCourseRepository();
            var dbData = await GetStudentCourseByCourseId(courseId);
            var addData = studentIds.Where(x => !dbData.Any(y => y.StudentId == x))
                .Select(x => new StudentCourse()
                {
                    StudentId = x,
                    CourseId = courseId
                }).ToList();
            var deleteData = dbData.Where(x => !studentIds.Any(y => y == x.StudentId));
            await repo.AddAsync(addData);
            repo.Delete(deleteData);
            return await uow.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpsertScoreByStudent(Guid studentId, IEnumerable<StudentCourse> studentCourses)
        {
            var repo = GetStudentCourseRepository();
            var dbData = repo.GetList(x => x.StudentId == studentId);
            var isStudentCoursesAllInDb = studentCourses.All(x => dbData.Any(y => y.CourseId == x.CourseId));
            if (!isStudentCoursesAllInDb)
            {
                return false;
            }
            var studentCoursesDictionary = studentCourses.ToDictionary(x => x.CourseId, x => x);
            foreach (var item in dbData)
            {
                if (studentCoursesDictionary.TryGetValue(item.CourseId, out var value))
                {
                    item.Score = value.Score;
                    repo.Update(item);
                }
            }
            return await uow.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpsertScoreByCourse(Guid courseId, IEnumerable<StudentCourse> studentCourses)
        {
            var repo = GetStudentCourseRepository();
            var dbData = repo.GetList(x => x.CourseId == courseId);
            var isStudentCoursesAllInDb = studentCourses.All(x => dbData.Any(y => y.StudentId == x.StudentId));
            if (!isStudentCoursesAllInDb)
            {
                return false;
            }
            var studentCoursesDictionary = studentCourses.ToDictionary(x => x.StudentId, x => x);
            foreach (var item in dbData)
            {
                if (studentCoursesDictionary.TryGetValue(item.StudentId, out var value))
                {
                    item.Score = value.Score;
                    repo.Update(item);
                }
            }
            return await uow.SaveChangesAsync() > 0;
        }

        private IGenericRepository<StudentCourse> GetStudentCourseRepository()
        {
            return uow.GetGenericRepository<StudentCourse>();
        }
    }
}
