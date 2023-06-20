using DomainLogic.Common;

namespace Logic.Domain.Users.ValueObjects
{
    public class Money : ValueObject<Money>
    {
        public const int MAXIMUMBALANCE = 1000000;
        public bool IsZero => Value == 0;
        public decimal Value { get; }
        protected Money() { }
        public Money(decimal moneyAmount) => Value = moneyAmount;
        public static Result<Money> AddBalance(decimal moneyAmount)
        {
            if (moneyAmount < 0) return Result.Fail<Money>("Money Amount Can not be negative");

            if (moneyAmount > MAXIMUMBALANCE) return Result.Fail<Money>("Money Amount Can not be negative");

            return Result.Ok(new Money(moneyAmount));
        }
        public override bool GetEqualCore(Money other) => Value == other.Value;

        public static implicit operator decimal(Money moneyAmount) => moneyAmount.Value;

        public static explicit operator Money(decimal moneyAmount) => AddBalance(moneyAmount).Value;
        public static Money operator *(Money money1, decimal money2) => AddBalance(money1.Value * money2).Value;
        public static Money operator +(Money money1, decimal money2) => AddBalance(money1.Value + money2).Value;
    }
}
