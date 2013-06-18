using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using FluentAssertions;
using NUnit.Framework;

namespace WebApiProblem.Tests.UnitTests
{
    [TestFixture]
    public class ResponseExceptionFilterAttributeTests
    {
        [TestCase("application/xml", "application/xml; charset=utf-8")]
        [TestCase("application/json", "application/json; charset=utf-8")]
        [TestCase("application/x-myCrazyStuff", "application/json; charset=utf-8")]
        public void TestContentNegotiation(string contentType, string expectedResponseContentType)
        {
            // Arrange
            var exception = new BasicApiProblemException(HttpStatusCode.BadRequest, "", "");
            var filter = new ResponseExceptionFilterAttribute(ResponseExceptionFormat.Negotiate);

            var executedContext = CreateContext(exception, r => r.Headers.Add("Accept", contentType));

            // Act
            filter.OnException(executedContext);

            // Assert
            executedContext.Response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            executedContext.Response.Content.Headers.ContentType.ToString().Should().Be(expectedResponseContentType);
        }

        private static HttpActionExecutedContext CreateContext(BasicApiProblemException exception, Action<HttpRequestMessage> setRequest = null)
        {
            var config = new HttpConfiguration();

            var httpRouteData = new HttpRouteData(new HttpRoute());
            var request = new HttpRequestMessage(HttpMethod.Get, "http://example.com");
            request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            if (setRequest != null)
            {
                setRequest(request);
            }

            var controllerContext = new HttpControllerContext(config,
                                                              httpRouteData,
                                                              request);

            var context = new HttpActionContext(controllerContext, new ReflectedHttpActionDescriptor());
            var executedContext = new HttpActionExecutedContext(context, exception);
            return executedContext;
        }
    }
}
