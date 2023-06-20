using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using DomainLogic.Common;
using DomainLogic.Courses.Entities;
using DomainLogic.Users.ValueObjects;

namespace DomainLogic.Users.Entities
{
    public class UserCourseEnrolment : Entity
    {
        private DateTime? _statusExpirationDate;
        private decimal _price;
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

            PurchaseDate = DateTime.UtcNow;
        }
        public Money Price { get => (Money)_price; set => _price = value; }
        public virtual ExpirationDate StatusExpirationDate
        {
            get => (ExpirationDate)_statusExpirationDate;
            set => _statusExpirationDate = value;
        }
        public Course Course { get; set; }
        public User User { get; set; }
        public virtual DateTime PurchaseDate { get; protected set; }

    }
}
