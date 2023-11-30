using System.Net;

namespace Voyager.Persistence.Exceptions
{
    public class AccountException : Exception
    {
        public int StatusCode { get; set; }
        public string CustomMessage { get; set; }
        public AccountException(string message):base(message) 
        {
            CustomMessage = message;
            StatusCode = (int)HttpStatusCode.BadRequest;
        }
    }
}
