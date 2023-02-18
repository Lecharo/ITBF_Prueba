using APPLogin.Models;

namespace APPLogin.Repository
{
    public interface IUserRepository
    {
        Task<int> Register(User user, string password);
        Task<string> Login(string userName, string password);
        Task<bool> UserExist(string userName);
    }
}
