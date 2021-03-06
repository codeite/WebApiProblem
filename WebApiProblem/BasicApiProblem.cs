using System.Runtime.Serialization;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace WebApiProblem
{
    [DataContract(Name = "problem", Namespace = WebApiProblemNames.XmlNamespaceName)]
    [XmlRoot("problem", Namespace = WebApiProblemNames.XmlNamespaceName)]
    public class BasicApiProblem : ApiProblem
    {
        [XmlElement("problemType")]
        [JsonProperty("problemType")]
        [DataMember(Name = "problemType", Order = 100, EmitDefaultValue = false)]
        public string ProblemTypeUrl { get; set; }

        [XmlElement("title")]
        [JsonProperty("title")]
        [DataMember(Name = "title", Order = 200, EmitDefaultValue = false)]
        public string Title { get; set; }

        [XmlElement("detail")]
        [JsonProperty("detail")]
        [DataMember(Name = "detail", Order = 300, EmitDefaultValue = false)]
        public string Detail { get; set; }

        [XmlElement("httpStatus")]
        [JsonProperty("httpStatus")]
        [DataMember(Name = "httpStatus", Order = 400, EmitDefaultValue = false)]
        public string HttpStatus { get; set; }

        [XmlElement("details")]
        [JsonProperty("details")]
        [DataMember(Name = "details", Order = 500, EmitDefaultValue = false)]
        public string Details { get; set; }

        [XmlElement("problemInstance")]
        [JsonProperty("problemInstance")]
        [DataMember(Name = "problemInstance", Order = 600, EmitDefaultValue = false)]
        public string ProblemInstance { get; set; }

    }
}