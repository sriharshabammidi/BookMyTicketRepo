using AutoMapper;
using BookMyTicket.Core;
using BookMyTicket.Entities;
using BookMyTicket.Interfaces.Repositories;
using BookMyTicket.Interfaces.Services;
using BookMyTicket.Models;
using BookMyTicket.Models.Core;
using System;
using System.Linq;

namespace BookMyTicket.BLL
{
    public class UserService : IUserService
    {
        private IUsersRepository _userRepository { get; set; }
        private JwtIssuerOptions _jwtIssuerOptions { get; set; }
        private readonly IMapper _mapper;

        public UserService(IUsersRepository userRepository, JwtIssuerOptions jwtIssuerOptions,
                           IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtIssuerOptions = jwtIssuerOptions;
        }

        public UserProfile GetUserById(long userId)
        {
            return _mapper.Map<UserProfile>(_userRepository.GetUser(userId));
        }
        public bool AddUser(UserProfile userProfile)
        {
            var users = _userRepository.GetByCondition(user => user.Email.ToLower() == userProfile.Email.ToLower());
            if (users.Any()) {
                throw new AppValidationException($"User with the email {userProfile.Email} already exists!");
            }
            userProfile.Password = EncryptionHelper.GetMd5Hash(userProfile.Password);
            return _userRepository.AddUser(_mapper.Map<User>(userProfile)).ID > 0;
        }
        public UserProfile Login(string email, string password)
        {
           var users =  _userRepository.GetByCondition(user => user.Email.ToLower() == email.ToLower());
            if (users.Count() == 0) {
                throw new UnauthorizedException("User not found");
            }
            var user = users.First();
            if (!EncryptionHelper.VerifyMd5Hash(password, user.Password)) {
                throw new UnauthorizedException("Invalid Credentitals!");
            }
            var userProfile = _mapper.Map<UserProfile>(user);
            userProfile.Token = JwtTokenHelper.GenerateJSONWebToken(_jwtIssuerOptions,user.ID);
            return userProfile;
        }
    }
}
