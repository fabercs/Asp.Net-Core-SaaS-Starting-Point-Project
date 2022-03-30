using System;
using System.Collections.Generic;

namespace EMSApp.Shared
{
    public class Response
    {
        public bool IsSuccess { get; set; }
        public string Message { get; private set; }
        public List<Error> Errors { get; set; } = new List<Error>();

        protected Response(bool isSuccess, List<Error> errors, string message)
        {
            if (isSuccess && errors?.Count > 0)
                throw new InvalidOperationException();

            if (!isSuccess && errors?.Count == 0)
                throw new InvalidOperationException();

            IsSuccess = isSuccess;
            Errors = errors;
            Message = message;
        }

        public static Response Ok() => new(true, default, null);
        public static Response Fail(List<Error> errors, string message = null) => new(false, errors, message);
        public static Response<T> Ok<T>(T data) => new(data, true, default, null);
        public static Response<T> Fail<T>(List<Error> errors, string message = null) =>
            new(default, false, errors, message);
    }

    public class Response<T> : Response
    {
        private readonly T _data;

        public T Data
        {
            get
            {
                if (!IsSuccess)
                    throw new InvalidOperationException("Can't return data if response.IsSuccess is false!");

                return _data;
            }
        }

        protected internal Response(T data, bool isSuccess, List<Error> errors, string message)
            : base(isSuccess, errors, message)
        {
            _data = data;
        }
    }
}
