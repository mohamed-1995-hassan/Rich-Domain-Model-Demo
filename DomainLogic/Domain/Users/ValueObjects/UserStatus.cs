using DomainLogic.Common;
using Logic.Domain.Users.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Logic.Domain.Users.ValueObjects
{
    public class UserStatus : ValueObject<UserStatus>
    {
        protected UserStatus() { }
        public UserStatus(UserOfferingType userOfferingType, ExpirationDate expirationDate) : this()
        {
            Type = userOfferingType;
            _expirationDate = expirationDate ?? throw new ArgumentException(nameof(expirationDate));
        }
        public UserOfferingType Type { get; }

        private DateTime? _expirationDate;
        public virtual ExpirationDate ExpirationDate { get => (ExpirationDate)_expirationDate; set => _expirationDate = value; }
        public decimal GetDiscount() => IsAdvanced ? .25m : 0m;
        public bool IsAdvanced => Type == UserOfferingType.Advanced && !ExpirationDate.IsExpired;
        public UserStatus Promote()
        {
            return new UserStatus(UserOfferingType.Advanced, (ExpirationDate)DateTime.UtcNow.AddYears(1));
        }
        public override bool GetEqualCore(UserStatus other) => Type == other.Type && ExpirationDate == other.ExpirationDate;
    }
}
