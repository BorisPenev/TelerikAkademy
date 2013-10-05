using System.IO;
using System.Xml.Serialization;

namespace MongodbDemo.Data.Helpers
{
    public class Helpers
    {
        public static bool DirectoryExist(string directoryPatch)
        {
            DirectoryInfo objDirectory = new DirectoryInfo(directoryPatch);
            if (objDirectory.Exists)
            {
                return true;
            }
            else
            {
                try
                {
                    Directory.CreateDirectory(directoryPatch);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

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
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                var ser = new XmlSerializer(typeof(T));
                result = (T)ser.Deserialize(fs);
            }
            return result;
        }
    }
}
