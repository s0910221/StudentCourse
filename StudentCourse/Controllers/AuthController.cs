using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace StudentCourse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly ITokenService tokenService;

        public AuthController(IUserService userService, ITokenService tokenService)
        {
            this.userService = userService;
            this.tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            var user = await userService.GetUser(loginVM);
            if (user == null)
            {
                return BadRequest("帳號或密碼有誤");
            }
            var token = tokenService.BuildUserToken(user);
            return Ok(token);
        }
    }
}