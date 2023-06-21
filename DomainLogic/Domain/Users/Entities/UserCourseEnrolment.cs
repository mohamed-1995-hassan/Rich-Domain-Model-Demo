using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using DomainLogic.Common;
using Logic.Domain.Courses.Entities;
using Logic.Domain.Users.ValueObjects;

namespace Logic.Domain.Users.Entities
{
    public class UserCourseEnrolment : Entity
    {
        protected UserCourseEnrolment()
        {

        }
        internal UserCourseEnrolment(User user, Course course, Money price, ExpirationDate expirationDate)
        {
            if (price == null || price.IsZero)
                throw new ArgumentException(nameof(price));

            if (expirationDate == null || expirationDate.IsExpired)
                throw new ArgumentException(nameof(expirationDate));

            Course = course ?? throw new ArgumentNullException(nameof(course));

            User = user ?? throw new ArgumentNullException(nameof(user));

            Price = price;

            StatusExpirationDate = expirationDate;

            EnrolmentDate = DateTime.UtcNow;
        }
        public Money Price { get; set; }
        public virtual ExpirationDate StatusExpirationDate { get; set; }
        public Course Course { get; set; }
        public User User { get; set; }
        public virtual DateTime EnrolmentDate { get; protected set; }

    }
}
