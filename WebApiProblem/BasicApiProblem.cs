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

        [XmlElement("httpStatus")]
        [JsonProperty("httpStatus")]
        public string HttpStatus { get; set; }

        [XmlElement("details")]
        [JsonProperty("details")]
        public string Details { get; set; }

        [XmlElement("problemInstance")]
        [JsonProperty("problemInstance")]
        public string ProblemInstance { get; set; }

    }
}