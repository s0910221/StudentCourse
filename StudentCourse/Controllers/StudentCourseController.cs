using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace StudentCourse.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentCourseController : ControllerBase
    {
        private readonly IStudentCourseService studentCourseService;

        public StudentCourseController(IStudentCourseService studentCourseService)
        {
            this.studentCourseService = studentCourseService;
        }

        /// <summary>
        /// 取得學生的課程清單
        /// </summary>
        // GET: api/StudentCourse/Student/{studentId}
        [HttpGet("Student/{studentId}")]
        public async Task<IActionResult> GetStudentCourseByStudentId(Guid studentId)
        {
            return Ok(await studentCourseService.GetStudentCourseByStudentId(studentId));
        }

        /// <summary>
        /// 更新學生的修課清單
        /// </summary>
        // PATCH: api/StudentCourse/Student/{studentId}
        [HttpPatch("Student/{studentId}")]
        public async Task<IActionResult> UpsertStudentCourseByStudent(Guid studentId, IEnumerable<Guid> courseIds)
        {
            var result = await studentCourseService.UpsertStudentCourseByStudent(studentId, courseIds);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest("更新修課清單失敗");
            }
        }

        /// <summary>
        /// 取得課程的修課學生清單
        /// </summary>
        // GET: api/StudentCourse/Course/{courseId}
        [HttpGet("Course/{courseId}")]
        public async Task<IActionResult> GetStudentCourseByCourseId(Guid courseId)
        {
            return Ok(await studentCourseService.GetStudentCourseByCourseId(courseId));
        }

        /// <summary>
        /// 更新課程的修課學生清單
        /// </summary>
        // PATCH: api/StudentCourse/Course/{courseId}
        [HttpPatch("Course/{courseId}")]
        public async Task<IActionResult> UpsertStudentCourseByCourse(Guid courseId, IEnumerable<Guid> studentIds)
        {
            var result = await studentCourseService.UpsertStudentCourseByCourse(courseId, studentIds);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest("更新修課清單失敗");
            }
        }
       
    }
}