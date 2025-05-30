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



        private readonly ApplicationDbContext _dbContext;

        public Auth(IRepository<User> _repo , ApplicationDbContext context)
        {
            _usersRepository = _repo;
            _dbContext = context;
        }


        public async Task<(int statusCode, bool success)> UserSignIn(SignInReq req)
        {
            try
            {
                var user = await _usersRepository.GetByEmail(req.Email!);

                if (user == null || user.Password != req.Password)
                {
                    return (401, false);
                }
                return (200, true);
            }
            catch
            {
                return (500, false);
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
    }

}
