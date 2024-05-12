using System.Net;
using System.Net.Sockets;
using DataTransporting;
using Models;

namespace AdminModule.Resources.Methods
{
    public class EditTerminalCommand
    {
        public static bool Edit(Terminal _terminal, IPEndPoint _ep)
        {
            try
            {
                Request request = new()
                {
                    Message = "EDITTERMINAL",
                    Terminal = _terminal
                };

                using (var client = new TcpClient())
                {
                    client.Connect(_ep);
                    using (var ns = client.GetStream())
                    {
                        ByteTransporting.SendBinary(ns, request);

                        Response response = (Response)ByteTransporting.GetBinary(ns);

                        if (response.Message == "EDITED")
                        {
                            return true;
                        }
                        else
                            return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            }
            return false;
        }
    }
}
