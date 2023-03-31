using MusicAPI.Models;

namespace MusicAPI.Interfaces
{
    public interface IUserRepository
    {
        bool CreateUser(User user);
        bool DeleteUser(User user);
        User GetUser(int userId);
        User GetUser(string username);
        bool UserExists(int userId);
        bool UserExists(string username);
        bool UserExists(string username, string password);
    }
}
