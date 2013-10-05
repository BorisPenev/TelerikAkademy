using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Bookstore.Model.Utils
{
    public static class Helpers
    {
        public static void SerializeToXml<T>(T source, string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                var ser = new XmlSerializer(typeof(T));
                var xns = new XmlSerializerNamespaces();
                xns.Add(string.Empty, string.Empty);
                ser.Serialize(fileStream, source, xns);
            }
        }

        public static T DeserializeFromXml<T>(string filePath)
        {
            T result;
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                var ser = new XmlSerializer(typeof(T));
                result = (T)ser.Deserialize(fs);
            }
            return result;
        }

        public static string SerializeToXmlToString<T>(T source)
        {
            string result;

            var settings = new XmlWriterSettings {OmitXmlDeclaration = true};
            var sw = new StringWriter();

            using (XmlWriter writer = XmlWriter.Create(sw, settings))
            {
                var ser = new XmlSerializer(typeof(T));
                var xns = new XmlSerializerNamespaces();
                xns.Add(string.Empty, string.Empty);
                ser.Serialize(writer, source, xns);
                result = sw.ToString();
            }
           
            return result;
        }
    }
}
