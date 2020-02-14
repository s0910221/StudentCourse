using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace StudentCourse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService courseService;

        public CourseController(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        /// <summary>
        /// 取得所有課程
        /// </summary>
        // GET: api/Course
        [HttpGet("")]
        public async Task<IActionResult> GetCourses()
        {
            return Ok(await courseService.GetCourses());
        }

        /// <summary>
        /// 新增課程
        /// </summary>
        // POST: api/Course
        [HttpPost("")]
        public async Task<IActionResult> CreateCourse(Course course)
        {
            var result = await courseService.CreateCourse(course);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest("新增課程失敗");
            }
        }

        /// <summary>
        /// 修改課程
        /// </summary>
        // PUT: api/Course/{courseId}
        [HttpPut("{courseId}")]
        public async Task<IActionResult> UpdateCourse(Guid courseId, Course course)
        {
            if (courseId != course.CourseId)
            {
                return BadRequest("資料不對");
            }
            var result = await courseService.UpdateCourse(course);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest("修改課程失敗");
            }
        }

        /// <summary>
        /// 刪除課程
        /// </summary>
        // DELETE: api/Course/{courseId}
        [HttpDelete("{courseId}")]
        public async Task<IActionResult> DeleteCourse(Guid courseId)
        {
            var result = await courseService.DeleteCourse(courseId);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest("新增課程失敗");
            }
        }

    }
}