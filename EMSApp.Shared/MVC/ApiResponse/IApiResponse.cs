using System;
using System.Collections.Generic;

namespace EMSApp.Shared
{
    public interface IApiResponse
    {
        ResponseStatus Status { get; }
        IEnumerable<string> Errors { get; }
        List<ValidationError> ValidationErrors { get; }
        Type? ValueType { get; }
        object? GetValue();
    }
}
