using System.Net;

namespace Server.Credentials
{
    public class ConnectionCredentialsRepository
    {
        private static readonly int _port;
        private static readonly IPAddress? _ip;
        private static readonly IPEndPoint? _ep;
        public static IPEndPoint? EP { get { return _ep; } }

        static ConnectionCredentialsRepository()
        {
            _port = 9001;
            _ip = IPAddress.Parse("127.0.0.1");
            _ep = new(_ip, _port);
        }
    }
}
