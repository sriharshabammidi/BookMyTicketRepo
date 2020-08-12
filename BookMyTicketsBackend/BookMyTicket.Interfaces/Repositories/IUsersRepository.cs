using BookMyTicket.Entities;
using System;
using System.Collections.Generic;

namespace BookMyTicket.Interfaces.Repositories
{
    public interface IUsersRepository
    {
        User AddUser(User user);

        User GetUser(long userId);

        List<User> GetUsers();

        User UpdateUser(User user);

        void RemoveUser(int userId);

        List<User> GetByCondition(Func<User, bool> condition);

        void Save();
    }
}
