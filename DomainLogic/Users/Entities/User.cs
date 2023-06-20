using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DomainLogic.Common;
using DomainLogic.Courses.Entities;
using DomainLogic.Users.ValueObjects;

namespace DomainLogic.Users.Entities
{
    public class User : Entity
    {
        private string _name;
        private string _email;
        private decimal _moneySpent;
        public virtual UserName Name { get => (UserName)_name ; set => _name = value; }
        public virtual Email Email { get => (Email)_email ; set => _email = value; }
        public virtual Money MoneySpent { get => (Money)_moneySpent ; protected set => _moneySpent = value; }
        public virtual UserStatus Status { get; set; }
        protected User() => _userCourseEnrolments = new List<UserCourseEnrolment>();

        public void EnrolToCourse(Course course)
        {
            if (HasEnroledCourses(course))
                throw new Exception();

            ExpirationDate expirationDate = course.GetExpirationDate();
            Money price = course.CalculatePrice(Status);
            var userCourseEnrolment = new UserCourseEnrolment(this, course, price, expirationDate);
            _userCourseEnrolments.Add(userCourseEnrolment);

            MoneySpent += price;
        }
        public virtual void Promote()
        {
            if (!CanPromote().IsSuccess)
                throw new Exception();

            Status = Status.Promote();
        }

        public virtual Result CanPromote()
        {
            if (Status.IsAdvanced)
                return Result.Fail("The customer is already has advanced status");

            if (UserCourseEnrolments.Count(x => x.StatusExpirationDate == ExpirationDate.Infinit ||
            x.StatusExpirationDate.Date >= DateTime.UtcNow.AddDays(-30)) < 2)
                return Result.Fail("at least 2 active movies during the last 30 days");

            if (UserCourseEnrolments.Where(x => x.PurchaseDate > DateTime.UtcNow.AddYears(-1)).Sum(x => x.Price) < 100m)
                return Result.Fail("at least 100 dollars spent during the last year");
            return Result.Ok();
        }

        public virtual bool HasEnroledCourses(Course movie) =>
            UserCourseEnrolments.Any(x => x.Course == movie && !x.StatusExpirationDate.IsExpired);

        private readonly IList<UserCourseEnrolment> _userCourseEnrolments;

        public IReadOnlyList<UserCourseEnrolment> UserCourseEnrolments => _userCourseEnrolments.ToList();

    }
}
