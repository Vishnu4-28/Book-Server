using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;
using System.Text;
using E_commerce.Server.Model.DTO;
using E_commerce.Server.Model.Entities;
using E_commerce.Server.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using static System.Collections.Specialized.BitVector32;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace E_commerce.Server.Controllers
{

    [EnableCors("AllowLocalhost3000")]
    [ApiController]
    [Route("[controller]/[Action]")]
    public class AuthController : Controller 
    {

        private readonly IAuth _authService;
        private readonly IConfiguration _configuration;

        private readonly BooksController _book;

        public AuthController(IAuth auth, IConfiguration configuration, BooksController book)
        {
            _authService = auth;
            _configuration = configuration;
            _book = book;
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

       
            var user = result.Users.FirstOrDefault(); 
            

            if (user == null)
            {
                return StatusCode(500, new
                {
                    statusCode = 500,
                    message = "Unexpected error: User not found"
                });
            }

            var token = GenerateJwtToken(user);

            string msg = "user SignIn successfully";
            _book.SendNotification(msg);

            return Ok(new
            {
                statusCode = 200,
                message = "Sign-in successful",
                user = new
                {
                    user.User_Id,
                    user.Email,
                    user.Role,
                    user.First_Name,
                    user.Last_Name
                },
                token
            });
        }



        [HttpPost(Name = "UploadProfile")]
        public async Task<IActionResult> uploadImage( FileUploadDTO model)
        {
            try
            {
                if (model.MyImage == null || model.MyImage.Length == 0)
                {
                    return BadRequest("No file uploaded");
                }
                var uploadedFile = await _authService.PostFileAsync(
                model.MyImage,
                model.ImageCaption,
                model.ImageDescription,
                model.Book_id 
                );

                return Ok(new
                {
                    message = "File uploaded successfully",
                    file = uploadedFile
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong: " + ex.Message);
            }
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


        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("role", user.Role.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var token = new JwtSecurityToken(
                 _configuration["Jwt:Issuer"],
                 _configuration["Jwt:Audience"],
                 claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpireMinutes"])),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpGet("GetNegotiationFile")]
        public IActionResult GetNegotiationFile()
        {
            List<string> add = new List<string>();

           var data = new Dictionary<string, string>();


            data["one"] = "1";
            data["two"] = "2";


            add.Add("susanta0");
            add.Add("susanta1");
            add.Add("susanta2");    
            add.Add("susanta3");

            return Ok(add);
        }



        [HttpGet("export-protobuf")]
        public IActionResult ExportUserProto()
        {
            var userRes = new protobuffUser { User_Id = 1, Email = "test@example.com" };
            byte[] protoBytes = ProtoSerializer.ProtoSerialize(userRes);
            return File(protoBytes, "application/x-protobuf", "user.bin");
        }


    }
}
