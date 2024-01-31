using Microsoft.EntityFrameworkCore;
using POLL.API.Data;
using POLL.API.Models;

namespace POLL.API.Services
{
    public class PollService : IPollService
    {
        private readonly ApplicationDbContext _context;

        public PollService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Poll> CreatePollAsync(Poll poll)
        {
            try
            {           
                if (poll == null)
                {
                    throw new ArgumentNullException(nameof(poll), "Poll object cannot be null");
                }
             
                _context.polls.Add(poll);
                await _context.SaveChangesAsync();

                return poll;
            }
            catch (Exception ex)
            {             
                throw new Exception("Failed to create poll", ex);
            }
        }

        public async Task<Poll> GetPollResultsAsync(int pollId)
        {
            try
            {
                
                var poll = await _context.polls
                    .Include(p => p.Options)  // Include related options
                    .FirstOrDefaultAsync(p => p.Id == pollId);

                if (poll == null)
                {
                   
                    return null; 
                  //  return NotFound();
                }

                return poll;
            }
            catch (Exception ex)
            {
              
                throw new Exception("Failed to get poll results", ex);
            }

        }
    }
}