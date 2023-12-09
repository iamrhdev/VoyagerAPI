using System.Net;

namespace Voyager.Persistence.Exceptions
{
    public class DuplicateNameException : Exception
    {
        public string CustomMessage { get; set; }
        public int StatusCode { get; set; }
        public DuplicateNameException(string message) : base(message) 
        {
            CustomMessage = message;    
            StatusCode = (int)HttpStatusCode.BadRequest;
        }
    }
}
