using E_commerce.Server.Model.DTO;
using E_commerce.Server.Model.Entities;
using E_commerce.Server.Service;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Server.Controllers
{


    [ApiController]
    [Route("[controller]/[Action]")]
    public class AuthController : Controller
    {

        private readonly IAuth _authService;

        public AuthController(IAuth auth)
        {
            _authService = auth;
        }


        [HttpPost(Name = "SignIn")]
        public async Task<IActionResult> SignIn(SignInReq req)
        {
            if (req == null || string.IsNullOrEmpty(req.Email) || string.IsNullOrEmpty(req.Password))
            {
                return BadRequest(new
                {
                    statusCode = 400,
                    message = "Invalid request data"
                });
            }

            var result = await _authService.UserSignIn(req);

            if (!result.success)
            {
                return StatusCode(result.statusCode, new
                {
                    statusCode = result.statusCode,
                    message = "Sign-in failed"
                });
            }

            return Ok(new
            {
                statusCode = 200,
                message = "Sign-in successful"
            });

        }



        [HttpPost(Name = "signup")]
        public async Task<IActionResult> Signup(UserReq req)
        {

            var errors = BookReqValidator.validateUser(req);
            if (errors.Any())
            {
                return BadRequest(new
                {
                    statusCode = 400,
                    message = "Validation failed",
                    errors
                });

            }


            if (req == null || string.IsNullOrEmpty(req.Email) || string.IsNullOrEmpty(req.Password))
            {
                return BadRequest(new
                {
                    statusCode = 400,
                    message = "Invalid request data"
                });
            }
            var result = await _authService.UserSignup(req);

            if (!result.success)
            {
                return StatusCode(result.statusCode, new
                {
                    statusCode = result.statusCode,
                    message = "Sign-in failed"
                });
            }
            return Ok(new
            {
                statusCode = result.statusCode,
                message = "Sign-in successful"
            });


        }
    }
}
