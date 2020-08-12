using AutoMapper;
using BookMyTicket.Interfaces.Repositories;
using BookMyTicket.Interfaces.Services;
using BookMyTicket.Models;

namespace BookMyTicket.BLL
{
    public class UserService : IUserService
    {
        public IUsersRepository _userRepository { get; set; }
        private readonly IMapper _mapper;

        public UserService(IUsersRepository userRepository,
                           IMapper mapper)
        {
            this._userRepository = userRepository;
            this._mapper = mapper;
        }

        public UserProfile GetUserById(long userId)
        {
            return _mapper.Map<UserProfile>(this._userRepository.GetUser(userId));
        }
    }
}
