using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using Newtonsoft.Json;

namespace WebApiProblem.Tests.Json
{
    internal static class DynamicJsonObjectFactory
    {
        private static void ObjectToCannonicalString(IEnumerable<KeyValuePair<string, dynamic>> jsonObject, bool cannonical, StringBuilder builder)
        {

            builder.Append("{");
            bool first = true;

            if (cannonical)
            {
                jsonObject = jsonObject.OrderBy(x => x.Key);
            }

            foreach (var o in jsonObject)
            {
                if (first)
                {
                    builder.Append("\"");
                    first = false;
                }
                else
                {
                    builder.Append(",\"");
                }

                builder.Append(o.Key);
                builder.Append("\":");
                ToCannonicalString(o.Value, builder);
            }

            builder.Append("}");
        }

        private static void ArrayToCannonicalString(IEnumerable<dynamic> jsonObject, StringBuilder builder)
        {
            builder.Append("[");
            bool first = true;

            foreach (var o in jsonObject)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    builder.Append(",");
                }

                ToCannonicalString(o, builder);
            }

            builder.Append("]");
        }


        public static object ToJsonString(this object jsonObject, StringBuilder builder = null)
        {
            return WrapBuilder(jsonObject, false, builder);
        }

        public static string ToCannonicalString(this object jsonObject, StringBuilder builder = null)
        {
            return WrapBuilder(jsonObject, true, builder);
        }

        private static string WrapBuilder(this object jsonObject, bool cannonical, StringBuilder builder = null)
        {
            bool madeBuilder = (builder == null);
            if (madeBuilder)
            {
                builder = new StringBuilder();
            }

            ToStringCommon(jsonObject, cannonical, builder);

            if (madeBuilder)
            {
                return builder.ToString();
            }

            return null;
        }

        private static void ToStringCommon(this object jsonObject, bool cannonical, StringBuilder builder)
        {
             if (jsonObject is IEnumerable<KeyValuePair<string, dynamic>>)
             {
                 ObjectToCannonicalString(jsonObject as IEnumerable<KeyValuePair<string, dynamic>>, cannonical, builder);
             }
             else if (jsonObject is IEnumerable<dynamic>)
             {
                 ArrayToCannonicalString(jsonObject as IEnumerable<dynamic>, builder);
             }
             else if (jsonObject is string)
             {
                 builder.Append("\"");
                 builder.Append(jsonObject as string);
                 builder.Append("\"");
             }
             else if (jsonObject is long || jsonObject is int || jsonObject is short || jsonObject is sbyte)
             {
                 builder.Append((long)jsonObject);
             }
             else if (jsonObject is ulong || jsonObject is uint || jsonObject is ushort || jsonObject is byte)
             {
                 builder.Append((ulong)jsonObject);
             }
             else if (jsonObject is float || jsonObject is double || jsonObject is decimal)
             {
                 builder.Append(jsonObject);
             }
             else if (jsonObject is BigInteger)
             {
                 builder.Append(jsonObject);
             }
             else if (jsonObject is bool)
             {
                 builder.Append(((bool)jsonObject)?"true":"false");
             }
             else if (jsonObject == null)
             {
                 builder.Append("null");
             }
             else
             {
                 throw new Exception("What do I do with a "+jsonObject.GetType());
             }
         }

        public static dynamic ReadJson(string json)
        {
            using (var stringReader = new StringReader(json))
            {
                return ReadJson(stringReader);
            }
        }

        public static dynamic ReadJson(TextReader reader)
        {
            return ReadJson(new JsonTextReader(reader));
        }

        public static dynamic ReadJson(JsonTextReader reader)
        {
            reader.Read();
            return ReadJsonValue(reader);
        }

        private static dynamic ReadJsonValue(JsonTextReader reader)
        {
            switch (reader.TokenType)
            {
                case JsonToken.String:
                case JsonToken.Integer:
                case JsonToken.Float:
                case JsonToken.Boolean:
                    var value = reader.Value;
                    reader.Read();
                    return value;
                case JsonToken.Null:
                    reader.Read();
                    return null;

                case JsonToken.StartObject:
                    return ReadJsonObject(reader);

                case JsonToken.StartArray:
                    return ReadJsonArray(reader);

                case JsonToken.None:
                case JsonToken.StartConstructor:
                case JsonToken.PropertyName:
                case JsonToken.Comment:
                case JsonToken.Raw:
                case JsonToken.Undefined:
                case JsonToken.EndObject:
                case JsonToken.EndArray:
                case JsonToken.EndConstructor:
                case JsonToken.Date:
                case JsonToken.Bytes:
                default:
                    throw new DynamicJsonObjectReadException("Expected value but got: "+ reader.TokenType.ToString());
            }
        }

        private static Dictionary<string, dynamic> ReadJsonObject(JsonTextReader reader)
        {
            var dictionary = new Dictionary<string, dynamic>();
            reader.Read();

            while (reader.TokenType != JsonToken.EndObject)
            {
                Assert(reader.TokenType == JsonToken.PropertyName, "End of object or next property name expected but got: " + reader.TokenType);
                var propertyName = (string)reader.Value;
                reader.Read();

                dictionary[propertyName] = ReadJsonValue(reader);
            }

            Assert(reader.TokenType == JsonToken.EndObject, "End of object expected but got: " + reader.TokenType);
            reader.Read();

            return dictionary;
        }

        private static List<dynamic> ReadJsonArray(JsonTextReader reader)
        {
            var list = new List<dynamic>();
            reader.Read();

            while (reader.TokenType != JsonToken.EndArray)
            {
                list.Add(ReadJsonValue(reader));
            }

            Assert(reader.TokenType == JsonToken.EndArray, "JsonToken.EndArray name expected but got: " + reader.TokenType);
            reader.Read();

            return list;
        }

        private static void Assert(bool condition, string message)
        {
            if (!condition)
            {
                throw new DynamicJsonObjectReadException(message);
            }
        }
    }
}

