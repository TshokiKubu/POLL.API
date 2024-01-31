
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using POLL.API.Models;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace POLL.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        internal object Polls;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Poll> polls { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PollOption> PollOptions { get; set; }
    }
}