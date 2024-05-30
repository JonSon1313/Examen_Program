using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace DataTransporting
{
    public class ByteTransporting
    {
        public static object GetBinary(NetworkStream ns)
        {
            return new BinaryFormatter().Deserialize(ns);
        }
        public static void SendBinary(NetworkStream ns, object data)
        {
            new BinaryFormatter().Serialize(ns, data);
        }
    }
}
