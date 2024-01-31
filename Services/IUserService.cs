using POLL.API.Models;

namespace POLL.API.Services
{
    public interface IUserService
    {
        Task<User> RegisterAsync(User user);
        Task<User> LoginAsync(User user);
    }
}
