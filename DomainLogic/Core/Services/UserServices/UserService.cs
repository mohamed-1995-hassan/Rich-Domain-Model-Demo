using DomainLogic.DataBase.Repositories;
using Logic.Core.Dtos.Users;
using Logic.Core.Extentions;
using Logic.Domain.Users.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Core.Services.UserServices
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public IEnumerable<UserInListDto> GetUsers()
        {
            IEnumerable<UserInListDto> users  = _userRepository
                .GetList()
                .Select(u=>u.ToUserDto())
                .ToList();
            return users;
        }
    }
}
