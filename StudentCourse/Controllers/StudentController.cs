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
    public class StudentController : ControllerBase
    {
        private readonly IStudentService studentService;
        private readonly IStudentCourseService studentCourseService;

        public StudentController(IStudentService studentService, IStudentCourseService studentCourseService)
        {
            this.studentService = studentService;
            this.studentCourseService = studentCourseService;
        }

        /// <summary>
        /// 取得所有學生
        /// </summary>
        // GET: api/Student
        [HttpGet("")]
        public async Task<IActionResult> GetStudents()
        {
            return Ok(await studentService.GetStudents());
        }

        /// <summary>
        /// 新增學生
        /// </summary>
        // POST: api/Student
        [HttpPost("")]
        public async Task<IActionResult> CreateStudent(Student student)
        {
            var result = await studentService.CreateStudent(student);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest("新增學生失敗");
            }
        }

        /// <summary>
        /// 修改學生
        /// </summary>
        // PUT: api/Student/{studentId}
        [HttpPut("{studentId}")]
        public async Task<IActionResult> UpdateStudent(Guid studentId, Student student)
        {
            if (studentId != student.StudentId)
            {
                return BadRequest("資料不對");
            }
            var result = await studentService.UpdateStudent(student);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest("修改學生失敗");
            }
        }

        /// <summary>
        /// 刪除學生
        /// </summary>
        // DELETE: api/Student/{studentId}
        [HttpDelete("{studentId}")]
        public async Task<IActionResult> DeleteStudent(Guid studentId)
        {
            var result = await studentService.DeleteStudent(studentId);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest("新增學生失敗");
            }
        }

        /// <summary>
        /// 批次更新學生修課成績
        /// </summary>
        //PATCH: api/Student/{studentId}/Score
        [HttpPatch("{studentId}/Score")]
        public async Task<IActionResult> UpsertStudentScore(Guid studentId, IEnumerable<Entity.Entity.StudentCourse> studentCourses)
        {
            var result = await studentCourseService.UpsertScoreByStudent(studentId, studentCourses);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest("更新學生修課成績失敗");
            }
        }
    }
}