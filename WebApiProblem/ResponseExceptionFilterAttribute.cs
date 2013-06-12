using System;
using System.Net.Http;
using System.Text;
using System.Web.Http.Filters;

namespace WebApiProblem
{
    public class ResponseExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ResponseExceptionFromat _responseExceptionFromat;

        public ResponseExceptionFilterAttribute() : this(ResponseExceptionFromat.Negotiate)
        {
        }

        public ResponseExceptionFilterAttribute(ResponseExceptionFromat responseExceptionFromat)
        {
            _responseExceptionFromat = responseExceptionFromat;
        }

        public sealed override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var ex = GetApiProblemException(actionExecutedContext.Exception);

            if (ex != null)
            {
                if (_responseExceptionFromat == ResponseExceptionFromat.Json)
                {
                    actionExecutedContext.Response = new HttpResponseMessage(ex.StatusCode)
                    {
                        Content =
                            new StringContent(ex.Serialize("application/json"), Encoding.UTF8,
                                              WebApiProblemNames.JsonMediaType)
                    };
                }
                else if( _responseExceptionFromat == ResponseExceptionFromat.Xml)
                {
                    actionExecutedContext.Response = new HttpResponseMessage(ex.StatusCode)
                    {
                        Content =
                            new StringContent(ex.Serialize("application/xml"), Encoding.UTF8,
                                              WebApiProblemNames.XmlMediaType)
                    };
                }
                else
                {
                    actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(ex.StatusCode,
                                                                                                  ex.ApiProblem);
                }
            }
            else
            {
                OnBasicException(actionExecutedContext);
            }
        }

        public virtual void OnBasicException(HttpActionExecutedContext actionExecutedContext)
        {
            
        }

        private ApiProblemException GetApiProblemException(Exception ex)
        {
            if (ex is ApiProblemException)
            {
                return ex as ApiProblemException;
            }

            if (ex.InnerException != null)
            {
                return GetApiProblemException(ex.InnerException);
            }

            return null;
        }
    }
}