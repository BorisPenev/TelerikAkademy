using System.IO;
using System.Xml.Serialization;

namespace CommandBinding.ImageGallery.Helpers
{
    public static class Utils
    {
        public static void SerializeToXml<T>(T obj, string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                var ser = new XmlSerializer(typeof(T));
                var xns = new XmlSerializerNamespaces();
                xns.Add(string.Empty, string.Empty);
                ser.Serialize(fileStream, obj, xns);
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
    }
}
