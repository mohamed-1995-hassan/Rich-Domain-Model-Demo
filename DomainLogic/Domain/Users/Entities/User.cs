
using DomainLogic.Common;
using Logic.Domain.Courses.Entities;
using Logic.Domain.Users.ValueObjects;
using System.Xml.Linq;

namespace Logic.Domain.Users.Entities
{
    public class User : Entity
    {
        public virtual UserName Name { get; protected set; }
        public virtual Email Email { get; protected set; }
        public virtual Money MoneySpent { get ; protected set; }
        public virtual UserStatus Status { get; protected set; }
        protected User() => _userCourseEnrolments = new List<UserCourseEnrolment>();
        public User(UserName name, Email email) : this()
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Status = UserStatus.Basic;
            MoneySpent = (Money)0;
        }
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

            if (UserCourseEnrolments.Where(x => x.EnrolmentDate > DateTime.UtcNow.AddYears(-1)).Sum(x => x.Price) < 100m)
                return Result.Fail("at least 100 dollars spent during the last year");
            return Result.Ok();
        }

        public virtual bool HasEnroledCourses(Course movie) =>
            UserCourseEnrolments.Any(x => x.Course == movie && !x.StatusExpirationDate.IsExpired);

        private readonly IList<UserCourseEnrolment> _userCourseEnrolments;

        public IReadOnlyList<UserCourseEnrolment> UserCourseEnrolments => _userCourseEnrolments.ToList();

        

    }
}
