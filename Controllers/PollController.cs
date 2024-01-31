using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POLL.API.Models;
using POLL.API.Services;

namespace POLL.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollController : ControllerBase
    {
        private readonly IPollService _pollService;

        public PollController(IPollService pollService)
        {
            _pollService = pollService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePoll([FromBody] Poll poll)
        {
            try
            {
                // Validate model
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Call the PollService to create a new poll
                var createdPoll = await _pollService.CreatePollAsync(poll);

                // Return the created poll with a 201 status code
                return CreatedAtAction(nameof(GetPollResults), new { pollId = createdPoll.Id }, createdPoll);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{pollId}")]
        public async Task<IActionResult> GetPollResults(int pollId)
        {
            try
            {
                // Call the PollService to get poll results
                var pollResults = await _pollService.GetPollResultsAsync(pollId);

                // Check if the poll results were found
                if (pollResults == null)
                {
                    return NotFound("Poll not found");
                }

                // Return the poll results
                return Ok(pollResults);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}