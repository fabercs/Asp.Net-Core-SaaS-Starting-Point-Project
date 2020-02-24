using System.Collections.Generic;

namespace EMSApp.Core.DTO.Responses
{
    public class Response<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<Error> Errors { get; set; } = new List<Error>();
       
    }
}
