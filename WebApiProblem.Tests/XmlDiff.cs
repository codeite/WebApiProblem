using System;
using System.IO;
using System.Xml;
using Microsoft.XmlDiffPatch;

namespace WebApiProblem.Tests
{
    public class XmlDiffAssert
    {
        public static void DiffXmlStrict(string actual, string expected)
        {
            XmlDocument expectedDoc;
            XmlDocument actualDoc;

            try
            {
                expectedDoc = new XmlDocument();
                expectedDoc.LoadXml(expected);
            }
            catch (XmlException e)
            {
                throw new Exception("Expected: " + e.Message + "\r\n" + expected);
            }

            try
            {
                actualDoc = new XmlDocument();
                actualDoc.LoadXml(actual);
            }
            catch (XmlException e)
            {
                throw new Exception("Actual: " + e.Message + "\r\n" + actual);
            }


            using (var stringWriter = new StringWriter())
            using (var writer = new XmlTextWriter(stringWriter))
            {
                writer.Formatting = Formatting.Indented;

                var xmldiff = new XmlDiff(XmlDiffOptions.None
                    //XmlDiffOptions.IgnoreChildOrder |
                    | XmlDiffOptions.IgnoreNamespaces
                    //XmlDiffOptions.IgnorePrefixes
                    );
                var identical = xmldiff.Compare(expectedDoc, actualDoc, writer);

                if (!identical)
                {
                    var error = string.Format("Expected:\r\n{0}\r\n\r\n" + "Actual:\r\n{1}\r\n\r\nDiff:\r\n{2}", 
                        expected, actual, stringWriter.GetStringBuilder());

                    throw new Exception(error);
                }
            }
        }
    }
}