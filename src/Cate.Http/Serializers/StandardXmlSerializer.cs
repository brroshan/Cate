using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Cate.Http.Serializers
{
    public class StandardXmlSerializer : ISerializer
    {
        public object Settings { get; set; }

        public T Deserialize<T>(string s)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var sr = new StringReader(s))
                return (T)serializer.Deserialize(sr);
        }

        public T Deserialize<T>(Stream s)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var sw = new StreamReader(s))
                return (T)serializer.Deserialize(sw);
        }

        public string Serialize<T>(T data)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var sw = new StringWriter())
            using (var xw = XmlWriter.Create(sw, Settings as XmlWriterSettings)) {
                serializer.Serialize(xw, data);
                return sw.ToString();
            }
        }
    }
}