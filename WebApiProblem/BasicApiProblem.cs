using System.Xml.Serialization;
using Newtonsoft.Json;

namespace WebApiProblem
{
    [XmlRoot("problem", Namespace = WebApiProblemNames.XmlNamespaceName)]
    public class BasicApiProblem : ApiProblem
    {
        [XmlElement("problemType")]
        [JsonProperty("problemType")]
        public string ProblemTypeUrl { get; set; }

        [XmlElement("title")]
        [JsonProperty("title")]
        public string Title { get; set; }

        [XmlElement("detail")]
        [JsonProperty("detail")]
        public string Detail { get; set; }

    }
}