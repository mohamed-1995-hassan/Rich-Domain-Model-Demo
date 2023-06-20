using Logic.Domain.Users.Entities;
using Logic.Domain.Users.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLogic.DataBase.Repositories
{
    public class UserRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IReadOnlyList<User> GetList()
        {
            return _applicationDbContext
                .Users
                .ToList();
        }

        public User GetByEmail(Email email)
        {
            return _applicationDbContext
                .Users
                .First(u => u.Email == email);
        }
    }
}
