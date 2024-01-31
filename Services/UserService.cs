using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using POLL.API.Data;
using POLL.API.Models;
using BCrypt.Net;

namespace POLL.API.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IJwtService _jwtService;

        public UserService(ApplicationDbContext context, IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        public async Task<User> RegisterAsync(User user)
        {
            try
            {               
                if (_context.Users.Any(u => u.Username == user.Username))
                {
                    throw new InvalidOperationException("Username is already taken.");
                }
               
                user.PasswordHash = HashPassword(user.PasswordHash);
                               
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return user;
            }
            catch (Exception ex)
            {              
                throw new Exception("User registration failed.", ex);
            }
        }

        public async Task<User> LoginAsync(User user)
        {
            try
            {               
                User? existingUser = await _context.Users.SingleOrDefaultAsync(u => u.Username == user.Username);
                               
                if (existingUser == null || !VerifyPassword(user.PasswordHash, existingUser.PasswordHash))
                {
                    throw new InvalidOperationException("Invalid username or password.");
                }
               
                string token = _jwtService.GenerateToken(existingUser);
                
                existingUser.Token = token;

                return existingUser;
            }
            catch (Exception ex)
            {
                
                throw new Exception("User login failed.", ex);
            }
        }
     
        private string HashPassword(string password)
        {          
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
               
        private bool VerifyPassword(string inputPassword, string hashedPassword)
        {            
            return BCrypt.Net.BCrypt.Verify(inputPassword, hashedPassword);
        }
    }
}