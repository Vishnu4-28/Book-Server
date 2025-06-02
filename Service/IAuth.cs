using E_commerce.Server.Model.DTO;
using E_commerce.Server.Model.Entities;

namespace E_commerce.Server.Service
{
    public interface IAuth
    {
        Task<(int statusCode, bool success)> UserSignup(UserReq req);

        Task<(int statusCode, IEnumerable<User>? Users, bool success)> UserSignIn(SignInReq req);

        Task<BookImg> PostFileAsync(IFormFile fileData, string imageCaption, string imageDescription , int Book_id);

    }
}
