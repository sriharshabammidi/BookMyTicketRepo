using BookMyTicket.DAL.Configurations;
using BookMyTicket.Entities;
using BookMyTicket.Interfaces.Repositories;
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
            var result = this.Insert(user);
            return result;
        }

        public User GetUser(long userId)
        {
            var result = this.GetById(userId);
            return result;
        }

        public List<User> GetUsers()
        {
            return this.GetAll();
        }

        public User UpdateUser(User user)
        {
            return this.Update(user);
        }

        public void RemoveUser(int userId)
        {
            this.Delete(userId);
        }
    }
}
