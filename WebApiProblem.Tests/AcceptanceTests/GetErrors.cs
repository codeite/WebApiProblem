using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using RestSharp;

namespace WebApiProblem.Tests.AcceptanceTests
{
    [TestFixture]
    class GetErrors
    {
        [Test]
        public void GetReturnsBadRequest()
        {
            // Arrange
            var client = new RestClient("http://localhost:61226");
            var request = new RestRequest("/Values", Method.GET);

            // Act
            IRestResponse response = client.Execute(request);
            
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public void GetReturnsJsonByDefault()
        {
            // Arrange
            var client = new RestClient("http://localhost:61226");
            var request = new RestRequest("/Values", Method.GET);

            // Act
            IRestResponse response = client.Execute(request);

            // Assert
            response.ContentType.Should().Be("application/json; charset=utf-8");
        }



        [TestCase("application/xml", "application/xml; charset=utf-8")]
        [TestCase("application/json", "application/json; charset=utf-8")]
        [TestCase("application/x-myCrazyStuff", "application/json; charset=utf-8")]
        public void GetReturnsRequestedType(string contentType, string expectedResponseContentType)
        {
            // Arrange
            var client = new RestClient("http://localhost:61226");
            var request = new RestRequest("/Values", Method.GET);
            request.AddHeader("Accept", contentType);

            // Act
            IRestResponse response = client.Execute(request);

            // Assert
            response.ContentType.Should().Be(expectedResponseContentType);
        }
    }
}
