using System;
using System.Collections.Generic;

namespace EMSApp.Shared
{

    /// <summary>
    /// Service response object for apis.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResponse<T> : IApiResponse
    {
        public ApiResponse(T value)
        {
            Value = value;
            if (Value != null)
                ValueType = Value.GetType();
        }

        public ApiResponse(ResponseStatus status)
        {
            Status = status;
        }
        public bool IsSuccess => Status == ResponseStatus.Ok;
        public string SuccessMessage { get; set; } = string.Empty;
        public ResponseStatus Status { get; private set; } = ResponseStatus.Ok;
        public IEnumerable<string> Errors { get; private set; } = new List<string>();
        public List<ValidationError> ValidationErrors { get; private set; } = new List<ValidationError>();
        public Type? ValueType { get; private set; }
        public T? Value { get; }
        public object? GetValue() => this.Value;

        public static ApiResponse<T> Success() => new(ResponseStatus.Ok);
        public static ApiResponse<T> Success(T value) => new(value);
        public static ApiResponse<T> Success(T value, string successMessage) =>
            new(value) { SuccessMessage = successMessage };
        public static ApiResponse<T> NotFound() => new(ResponseStatus.NotFound);
        public static ApiResponse<T> Unauthorized() => new(ResponseStatus.Unauthorized);
        public static ApiResponse<T> Forbidden() => new(ResponseStatus.Forbidden);
        public static ApiResponse<T> Error(params string[] errors) => new(ResponseStatus.Error) { Errors = errors };
        public static ApiResponse<T> Invalid(List<ValidationError> validationErrors)
            => new(ResponseStatus.Invalid) { ValidationErrors = validationErrors };

    }


}
