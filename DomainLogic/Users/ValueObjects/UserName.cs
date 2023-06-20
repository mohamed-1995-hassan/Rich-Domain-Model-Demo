using DomainLogic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLogic.Users.ValueObjects
{
    public class UserName : ValueObject<UserName>
    {
        public string Value { get; }
        private UserName(string value) => Value = value;
        public static Result<UserName> Create(string customerName)
        {
            customerName = (customerName ?? string.Empty).Trim();
            if (customerName.Length == 0)
                return Result.Fail<UserName>("Customer Name Should not be empty");

            if (customerName.Length > 100)
                return Result.Fail<UserName>("Customer Name Is Too Long");

            return Result.Ok(new UserName(customerName));
        }

        public override bool GetEqualCore(UserName other) => 
            Value.Equals(other.Value, StringComparison.InvariantCultureIgnoreCase);

        public static implicit operator string(UserName username) => username.Value;

        public static explicit operator UserName(string username) => Create(username).Value;
    }
}
