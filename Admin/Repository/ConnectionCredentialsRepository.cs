using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Repository
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
