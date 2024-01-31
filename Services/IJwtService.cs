using POLL.API.Models;

namespace POLL.API.Services
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
