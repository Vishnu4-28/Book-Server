using E_commerce.Server.DAL.BASE;
using E_commerce.Server.data;
using E_commerce.Server.Model.DTO;
using E_commerce.Server.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Server.Service
{
    public class Auth : IAuth
    {
        private readonly IRepository<User> _usersRepository;
        private readonly IWebHostEnvironment _environment;
        private readonly IRepository<BookImg> _bookImg;
        private readonly string _uploadFolder;


        private readonly ApplicationDbContext _dbContext;

        public Auth(IRepository<User> _repo , ApplicationDbContext context , IRepository<BookImg> bookImg , IWebHostEnvironment environment)
        {
            _usersRepository = _repo;
            _dbContext = context;
            _bookImg = bookImg;
            _environment = environment;

            if (string.IsNullOrEmpty(_environment.WebRootPath))
            {
                _environment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }

            // Create wwwroot directory if it doesn't exist
            if (!Directory.Exists(_environment.WebRootPath))
            {
                Directory.CreateDirectory(_environment.WebRootPath);
            }

            _uploadFolder = Path.Combine(_environment.WebRootPath, "FileUpload");

            // Create the upload folder if it doesn't exist
            if (!Directory.Exists(_uploadFolder))
            {
                Directory.CreateDirectory(_uploadFolder);
            }

        }


        public async Task<(int statusCode, IEnumerable<User>? Users, bool success)> UserSignIn(SignInReq req)
        {
            try
            {
                var user = await _usersRepository.GetByEmail(req.Email!);

                if (user == null || user.Password != req.Password)
                {
                    return (401, null, false);
                }
                return (200, new List<User> { user }, true);
            }
            catch
            {
                return (500, null, false);
            }
        }

        public async Task<(int statusCode, bool success)> UserSignup(UserReq req)
        {
            try
            {
                var user = new User
                {
                    First_Name = req.First_Name ?? "",
                    Last_Name = req.Last_Name ?? "",
                    Date_Of_Birth = req.Date_Of_Birth,
                    Email = req.Email ?? "",
                    Password = req.Password ?? "",
                    Phone_Number = req.Phone_Number ?? "",
                    Role = UserRole.User,
                };

                await _usersRepository.Add(user);

                return (200, true);
            }
            catch
            {
                return (500, false);
            }
        }

        public async Task<BookImg> PostFileAsync(IFormFile fileData, string imageCaption, string imageDescription , int Book_id)
        {
            try
            {
                var file = new BookImg
                {
                    Book_id = Book_id,
                    FileName = Path.GetFileNameWithoutExtension(fileData.FileName),
                    FileType = Path.GetExtension(fileData.FileName),
                    FileSize = fileData.Length,
                    ImageCaption = imageCaption,
                    ImageDescription = imageDescription,
                    UploadDate = DateTime.Now
 
                };

 
                var uniqueFileName = $"{Guid.NewGuid()}{file.FileType}";
               
                file.FilePath = Path.Combine("FileUpload", uniqueFileName);
                var fullPath = Path.Combine(_uploadFolder, uniqueFileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await fileData.CopyToAsync(stream);
                }

       
               await _bookImg.Add(file);
                

                return file;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}
