using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shouldly;
using WebApiProblem.Tests.Json;

namespace WebApiProblem.Tests
{
    internal static class JsonAssertHelper
    {
        public static void ShouldBeJson(this string actual, string expected)
        {
            var jsonCannonicalizer = new JsonCannonicalizer();
            
            var actualCanonical = jsonCannonicalizer.Cannonicalize(actual);
            var expectedCanonical = jsonCannonicalizer.Cannonicalize(expected);

            actualCanonical.ShouldBe(expectedCanonical);
        }
    }
}
