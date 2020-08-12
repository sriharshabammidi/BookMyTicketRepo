using BookMyTicket.Models;

namespace BookMyTicket.Interfaces.Services
{
    public interface IUserService
    {
        UserProfile GetUserById(long userId);
        bool AddUser(UserProfile userProfile);
        UserProfile Login(string email, string password);
    }
}
