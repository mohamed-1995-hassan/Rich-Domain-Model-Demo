using DomainLogic.Common;
using DomainLogic.Courses.Enums;
using DomainLogic.Users.ValueObjects;

namespace DomainLogic.Courses.Entities
{
    public abstract class Course : Entity
    {
        public string Name { get; set; }
        public virtual Money CalculatePrice(UserStatus status)
        {
            decimal modifier = 1 - status.GetDiscount();
            return GetBasePrice() * modifier;
        }
        public abstract Money GetBasePrice();
        public abstract ExpirationDate GetExpirationDate();
        public int LicensingModel { get; set; }
    }
    public class LongTimeCourse : Course
    {
        public override Money GetBasePrice() => (Money)10m;
        public override ExpirationDate GetExpirationDate() => ExpirationDate.Infinit;
    }
    public class ShortTimeCourse : Course
    {
        public override Money GetBasePrice() => (Money)2m;

        public override ExpirationDate GetExpirationDate() => (ExpirationDate)DateTime.UtcNow.AddMonths(2);
    }
}
