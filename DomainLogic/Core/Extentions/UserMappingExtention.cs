using Logic.Core.Dtos.Users;
using Logic.Domain.Users.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Core.Extentions
{
    public static class UserMappingExtention
    {
        public static UserInListDto ToUserDto(this User user)
        {
            return new UserInListDto
            {
                Id = user.Id,
                Name = user.Name.Value,
                Email = user.Email.Value,
                Status = user.Status.Type.ToString(),
                StatusExpirationDate = user.Status.ExpirationDate.Date,
                MoneySpent = user.MoneySpent
            };
        }
    }
}
