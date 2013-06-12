using System.IO;
using System.Net;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace WebApiProblem
{
    public class BasicApiProblemException : ApiProblemException<BasicApiProblem>
    {
        public BasicApiProblemException(HttpStatusCode statusCode, string title, string problemTypeUrl, string detail = null)
        {
            ApiProblem = new BasicApiProblem { Title = title, Detail = detail, ProblemTypeUrl = problemTypeUrl };
            StatusCode = statusCode;
        }

        public override string Serialize(string contentType)
        {
            if (contentType == "application/json")
            {
                return SerializeToJson();
            }

            return SerializeToXml();
        }

        private string SerializeToJson()
        {
            var serializer = JsonSerializer.Create(new JsonSerializerSettings());

            using(var writer = new StringWriter())
            {
                serializer.Serialize(writer, ApiProblem);
                var serialized = writer.GetStringBuilder().ToString();
                return serialized;
            }
        }

        private string SerializeToXml()
        {
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, WebApiProblemNames.XmlNamespaceName);

            var serializer = new XmlSerializer(typeof (BasicApiProblem));
            var encoding = Encoding.UTF8;

            using (var memStream = new MemoryStream())
            using (TextWriter writer = new StreamWriter(memStream, encoding))
            {
                serializer.Serialize(writer, ApiProblem, namespaces);
                var data = memStream.ToArray();
                var serialized = RemoveBom(encoding.GetString(data), encoding);
                return serialized;
            }
        }

        private static string RemoveBom(string p, Encoding encoding)
        {
            var bomMark = encoding.GetString(encoding.GetPreamble());
            if (p.StartsWith(bomMark))
                p = p.Remove(0, bomMark.Length);
            return p.Replace("\0", "");
        }
    }
}