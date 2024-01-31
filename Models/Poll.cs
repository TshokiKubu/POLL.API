namespace POLL.API.Models
{
    public class Poll
    {
         public int Id { get; set; }
         public string Question { get; set; }
         public ICollection<PollOption> Options { get; set; } = new List<PollOption>();

    }
}
