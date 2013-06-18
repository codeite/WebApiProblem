using FluentAssertions;
using WebApiProblem.Tests.Json;

namespace WebApiProblem.Tests.UnitTests
{
    internal static class JsonAssertHelper
    {
        public static void ShouldBeJson(this string actual, string expected)
        {
            var jsonCannonicalizer = new JsonCannonicalizer();
            
            var actualCanonical = jsonCannonicalizer.Cannonicalize(actual);
            var expectedCanonical = jsonCannonicalizer.Cannonicalize(expected);

            actualCanonical.Should().Be(expectedCanonical);
        }
    }
}
