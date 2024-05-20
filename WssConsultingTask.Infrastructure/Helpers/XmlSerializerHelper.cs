using System.Xml.Serialization;

using WssConsultingTask.Application.Interfaces;

namespace WssConsultingTask.Infrastructure.Helpers
{
    public class XmlSerializerHelper : IXmlSerializerHelper
    {
        public T Deserialize<T>(string xml)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));

            using var sr = new StringReader(xml);

            return (T)xmlSerializer.Deserialize(sr);
        }

        public byte[] Serialize<T>(T obj)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));

            using var stream = new MemoryStream();

            xmlSerializer.Serialize(stream, obj);

            return stream.ToArray();
        }
    }
}
