using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace BookmarkImporter.Utils
{
    public static class Extensions
    {
        public static string GetChildText(this XmlNode node, string tagName)
        {
            XmlNode childNode = node.SelectSingleNode(tagName);
            if (childNode == null)
            {
                return null;
            }
            return childNode.InnerText;
        }
    }
}
