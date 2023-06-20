using DomainLogic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Domain.Users.ValueObjects
{
    public class ExpirationDate : ValueObject<ExpirationDate>
    {
        protected ExpirationDate()
        {

        }
        public ExpirationDate(DateTime? date) => Date = date;
        public DateTime? Date { get; }
        public bool IsExpired => this != Infinit && Date < DateTime.UtcNow;
        public static Result<ExpirationDate> Create(DateTime date) => Result.Ok(new ExpirationDate(date));

        public static readonly ExpirationDate Infinit = new ExpirationDate(null);
        public override bool GetEqualCore(ExpirationDate other) => Date == other.Date;

        public static explicit operator ExpirationDate(DateTime? date) =>
            date.HasValue ? Create(date.Value).Value : Infinit;

        public static implicit operator DateTime?(ExpirationDate date) => date.Date;
    }
}
