namespace WssConsultingTask.Application.Interfaces
{
    public interface IXmlSerializerHelper
    {
        byte[] Serialize<T>(T obj);

        T Deserialize<T>(string xml);
    }
}
