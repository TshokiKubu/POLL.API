using POLL.API.Models;

namespace POLL.API.Services
{
    public interface IPollService
    {
        Task<Poll> CreatePollAsync(Poll poll);
        Task<Poll> GetPollResultsAsync(int pollId);
    }
}
