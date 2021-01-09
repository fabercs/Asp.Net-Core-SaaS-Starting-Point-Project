using System;
using System.Collections.Generic;

namespace EMSApp.Core.DTO.Responses
{
    public class Response
    {
        public bool IsSuccess { get; set; }
        public string Message { get; private set; }
        public List<Error> Errors { get; set; } = new List<Error>();

        protected Response(bool isSuccess, List<Error> errors, string message)
        {
            if (isSuccess && errors.Count > 0)
                throw new InvalidOperationException();
            if (!isSuccess && errors.Count == 0)
                throw new InvalidOperationException();

            IsSuccess = isSuccess;
            Errors = errors;
            Message = message;
        }

        public static Response Ok()
        {
            return new Response(true, null, null);
        }
        public static Response Fail(List<Error> errors, string message=null)
        {
            return new Response(false, errors, message);
        }
        public static Response<T> Ok<T>(T data)
        {
            return new Response<T>(data, true, null, null);
        }
        public static Response<T> Fail<T>(List<Error> errors, string message=null)
        {
            return new Response<T>(default, false, errors, message);
        }
    }

    public class Response<T> : Response
    {
        private readonly T _data;

        public T Data
        {
            get
            {
                if (!IsSuccess)
                    throw new InvalidOperationException();

                return _data;
            }
        }

        protected internal Response(T data, bool isSuccess, List<Error> errors, string message)
            :base(isSuccess, errors, message)
        {
            _data = data;
        }
    }



    
}
