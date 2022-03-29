using System.IO;
using System.Xml.Serialization;

namespace UIScripts
{
    public static class XMLHelper
    {
        public static void Serialize(object item, string path)
        {
            var serializer = new XmlSerializer(item.GetType());
            var writer = new StreamWriter(path);
            serializer.Serialize(writer.BaseStream, item);
            writer.Close();
        }
 
        public static T Deserialize<T>(string path)
        {
            var serializer = new XmlSerializer(typeof(T));
            var reader = new StreamReader(path);
            var deserialized = (T)serializer.Deserialize(reader.BaseStream);
            reader.Close();
            return deserialized;
        }
    }
}