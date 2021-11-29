using System;
 

namespace Cleverti.Todo.Application.Exceptions
{
    public class ApiError : Exception
    {
        public ApiError(int status, string message) : base(message)
        {
            Status = status;
        }

        public ApiError(string message) : this(500, message)
        {
        }
        
        public int Status { get; }
    }
}
