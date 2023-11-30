using System.Net;

namespace Voyager.Persistence.Exceptions
{
    public class DuplicateEmailException : Exception
    {
        public string CustomMessage { get; set; }
        public int StatusCode { get; set; }
        public DuplicateEmailException(string message) : base(message) 
        {
            CustomMessage = message;    
            StatusCode = (int)HttpStatusCode.BadRequest;
        }
    }
}
