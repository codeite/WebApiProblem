using System;
using System.Net;

namespace WebApiProblem
{
    public abstract class ApiProblemException<T> : Exception, ApiProblemException where T : ApiProblem
    {
        public HttpStatusCode StatusCode { get; protected set; }

        public ApiProblem ApiProblem { get; protected set; }

        public abstract string Serialize(string contentType);
    }

    public interface ApiProblemException
    {
        HttpStatusCode StatusCode { get; }
        string Serialize(string contentType);
        ApiProblem ApiProblem { get; }
    }

}