namespace WebApiProblem.Tests.Json
{
    internal class JsonCannonicalizer
    {
        public string Cannonicalize(string json)
        {
            return DynamicJsonObjectFactory.ToCannonicalString(DynamicJsonObjectFactory.ReadJson(json));
        }
    }
}
