using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using NUnit.Framework;

namespace WebApiProblem.Tests.UnitTests
{
    [TestFixture]
    public class TestSerialization
    {
        [Test]
        public void ASimpleExceptionShouldSerializeToXml()
        {
            // Arrange
            var exception = new BasicApiProblemException(
                HttpStatusCode.Forbidden,
                "You do not have enough credit.",
                "http://example.com/probs/out-of-credit",
                "Your current balance is 30, but that costs 50.");

            const string expected = 
                @"<?xml version=""1.0"" encoding=""utf-8""?>
                <problem xmlns=""urn:web-api-problem"">
                  <problemType>http://example.com/probs/out-of-credit</problemType>
                  <title>You do not have enough credit.</title>
                  <detail>Your current balance is 30, but that costs 50.</detail>
                </problem>";

            // Act
            var serialized = exception.Serialize("application/xml");

            // Assert
            XmlDiffAssert.DiffXmlStrict(serialized, expected);
        }

        [Test]
        public void ASimpleExceptionShouldSerializeToXmlUsingDataContractSerializer()
        {
            // Arrange
            var exception = new BasicApiProblemException(
                HttpStatusCode.Forbidden,
                "You do not have enough credit.",
                "http://example.com/probs/out-of-credit",
                "Your current balance is 30, but that costs 50.");

            const string expected =
                @"
                <problem xmlns=""urn:web-api-problem"">
                  <problemType>http://example.com/probs/out-of-credit</problemType>
                  <title>You do not have enough credit.</title>
                  <detail>Your current balance is 30, but that costs 50.</detail>
                </problem>";

            // Act
            var apiProblemType = exception.ApiProblem.GetType();

            var dataContractSerializer = new DataContractSerializer(apiProblemType);

            string serialized;
            using (var memStream = new MemoryStream())
            {
                dataContractSerializer.WriteObject(memStream,  exception.ApiProblem);
                var data = memStream.ToArray();
                serialized = Encoding.UTF8.GetString(data);
            }

            // Assert
            XmlDiffAssert.DiffXmlStrict(serialized, expected);
        }

        [Test]
        public void ASimpleExceptionShouldSerializeToJson()
        {
            // Arrange
            var exception = new BasicApiProblemException(
                HttpStatusCode.Forbidden,
                "You do not have enough credit.",
                "http://example.com/probs/out-of-credit",
                "Your current balance is 30, but that costs 50.");

            const string expected =@"
                {
                    ""problemType"": ""http://example.com/probs/out-of-credit"",
                    ""title"": ""You do not have enough credit."",
                    ""detail"": ""Your current balance is 30, but that costs 50."",
                }";

            // Act
            var serialized = exception.Serialize("application/json");

            // Assert
            serialized.ShouldBeJson(expected);
        }
    }
}
