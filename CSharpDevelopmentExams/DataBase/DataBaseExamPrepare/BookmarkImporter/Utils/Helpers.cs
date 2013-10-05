using System.IO;
using System.Xml.Serialization;

namespace BookmarkImporter.Utils
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
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                var ser = new XmlSerializer(typeof(T));
                result = (T)ser.Deserialize(fs);
            }
            return result;
        }
    }
}
