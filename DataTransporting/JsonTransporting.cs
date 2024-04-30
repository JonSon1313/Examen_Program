using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace DataTransporting
{
    public class JsonTransporting
    {
        public static T GetBinary<T>(NetworkStream ns)
        {
            return JsonSerializer.Deserialize<T>(ns);
        }

        public static void SendBinary<T>(NetworkStream ns, T data)
        {
            
            JsonSerializer.Serialize<T>(ns, data);
        }
    }
}
