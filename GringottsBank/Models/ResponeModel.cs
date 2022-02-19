using System.Collections.Generic;
using System.Linq;

namespace GringottsBank.Models
{
    public class Success<T> : ResponseModel<T>
    {
        public T Data { get; set; }
    }

    public class Error<T> : ResponseModel<T>
    {
        public string Message { get { return HasError ? string.Join(",", ErrorMessages) : ""; } }
    }
    public class ResponseModel<T>
    {
        internal bool IsAuthorized { get; set; } = true;
        internal List<string> ErrorMessages { get; set; } = new List<string>();
        internal bool HasError { get { return ErrorMessages is not null && ErrorMessages.Any(); } }
    }
}
