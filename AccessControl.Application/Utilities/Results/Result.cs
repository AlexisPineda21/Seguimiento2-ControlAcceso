namespace AccessControl.Application.Utilities.Results
{
    public class Result
    {
        private readonly List<Error> _errors = new();

        public bool IsSuccess => _errors.Count == 0;
        public bool IsFailure => !IsSuccess;
        public string? Message { get; }
        public IReadOnlyCollection<Error> Errors => _errors;

        protected Result(string? message)
        {
            Message = message;
        }

        protected Result(IEnumerable<Error> errors, string? message)
        {
            _errors.AddRange(errors);
            Message = message;
        }

        public static Result Success(string? message = "Operación realizada correctamente.")
        {
            return new Result(message);
        }

        public static Result Failure(Error error, string? message = "No fue posible completar la operación.")
        {
            return new Result([error], message);
        }

        public static Result Failure(IEnumerable<Error> errors, string? message = "No fue posible completar la operación.")
        {
            return new Result(errors, message);
        }
    }

    public class Result<T> : Result
    {
        public T? Value { get; }

        private Result(T value, string? message) : base(message)
        {
            Value = value;
        }

        private Result(IEnumerable<Error> errors, string? message) : base(errors, message)
        {
        }

        public static Result<T> Success(T value, string? message = "Operación realizada correctamente.")
        {
            return new Result<T>(value, message);
        }

        public static new Result<T> Failure(Error error, string? message = "No fue posible completar la operación.")
        {
            return new Result<T>([error], message);
        }

        public static new Result<T> Failure(IEnumerable<Error> errors, string? message = "No fue posible completar la operación.")
        {
            return new Result<T>(errors, message);
        }
    }
}
