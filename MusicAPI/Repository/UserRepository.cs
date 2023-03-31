using MusicAPI.Data;
using MusicAPI.Interfaces;
using MusicAPI.Models;

namespace MusicAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MusicDbContext _context;

        public UserRepository(MusicDbContext context)
        {
            _context = context;
        }

        public bool CreateUser(User user)
        {
            _context.Add(user);
            return Save();
        }

        public bool DeleteUser(User user)
        {
            _context.Remove(user);
            return Save();
        }

        public User GetUser(int userId)
        {
            return _context.Users.Where(u => u.Id == userId).FirstOrDefault();
        }
        
        public User GetUser(string username)
        {
            return _context.Users.Where(u => u.Username.Replace(" ", "").ToLower() == username.Replace(" ", "").ToLower()).FirstOrDefault();
        }

        public bool UserExists(int userId)
        {
            return _context.Users.Any(u => u.Id == userId);
        }

        public bool UserExists(string username)
        {
            return _context.Users.Any(u => u.Username.Trim().ToLower() == username.Trim().ToLower());
        }

        public bool UserExists(string username, string password)
        {
            return _context.Users.Any(u => u.Username == username && u.Password == password);
        }

        private bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
