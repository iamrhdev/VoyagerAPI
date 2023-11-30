using System.Net;

namespace Voyager.Persistence.Exceptions
{
    public class SignInException : Exception
    {
        public string CustomMessage { get; set; }
        public int StatusCode { get; set; }
        public SignInException(string message) : base(message)
        {
            CustomMessage = message;
            StatusCode = (int)HttpStatusCode.BadRequest;
        }
    }
}
