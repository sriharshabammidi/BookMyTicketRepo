using BookMyTicket.DAL.Configurations;
using BookMyTicket.Entities;
using BookMyTicket.Interfaces.Repositories;
using System;
using System.Collections.Generic;

namespace BookMyTicket.DAL
{
    public class UsersRepository : BaseRepository<User>, IUsersRepository
    {
        public UsersRepository(BookMyTicketDBContext dbContext)
            : base(dbContext)
        {
        }

        public User AddUser(User user)
        {
            var result = Insert(user);
            return result;
        }

        public User GetUser(long userId)
        {
            var result = GetById(userId);
            return result;
        }

        public List<User> GetUsers()
        {
            return GetAll();
        }

        public User UpdateUser(User user)
        {
            return Update(user);
        }

        public void RemoveUser(int userId)
        {
            Delete(userId);
        }

        public List<User> GetByCondition(Func<User, bool> condition)
        {
            return GetBy(condition);
        }
    }
}
