using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiProblem.TestWebApplication.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/<controller>
        public HttpResponseMessage Get()
        {
            throw new BasicApiProblemException(HttpStatusCode.BadRequest, "MyBadRequest", "http://example.com/errors/401");
        }
    }
}