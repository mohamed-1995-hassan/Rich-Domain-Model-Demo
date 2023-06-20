using DomainLogic.Common;
using System.Text.RegularExpressions;

namespace DomainLogic.Users.ValueObjects
{
    public class Email : ValueObject<Email>
    {
        public string Value { get; }
        public Email(string value) => Value = value;
        public override bool GetEqualCore(Email other) => Value == other.Value;
        public static Result<Email> Create(string email)
        {
            if (email.Length == 0) return Result.Fail<Email>("Email Should Not Be Empty");

            if (!Regex.IsMatch(email, @"^(.+)@(.+)$")) return Result.Fail<Email>("Email Is Invalid");

            return Result.Ok(new Email(email));
        }

        public static implicit operator string(Email email) => email.Value;

        public static explicit operator Email(string email) => Create(email).Value;
    }
}
