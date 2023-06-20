

namespace DomainLogic.Common
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string Error { get; set; }
        protected Result(bool isSuccess, string error)
        {
            if(!isSuccess && error == string.Empty) throw new InvalidOperationException();

            if (isSuccess && error != string.Empty) throw new InvalidOperationException();

            IsSuccess = isSuccess;
            Error = error;
        }
        public static Result Ok() => new Result(true, string.Empty);
        public static Result Fail(string message) => new Result(false,message);
        public static Result<T> Ok<T>(T value) => new Result<T>(value, true, string.Empty);
        public static Result<T> Fail<T>(string message) => new Result<T?>(default(T?), true, message);
    }
    public class Result<T> : Result
    {
        private readonly T _value;

        public T Value => !IsSuccess ? throw new InvalidOperationException() : _value;
        public Result(T value, bool isSuccess, string message) : base(isSuccess, message) => _value = value;
    }
}
