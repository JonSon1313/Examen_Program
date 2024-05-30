using DataTransporting;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientModule.Resources.Methods
{
    public class GetDiscount
    {
        public static double Get(int flightId, IPEndPoint _ep)
        {
            try
            {
                Request request = new()
                {
                    Message = $"GETDISCOUNT",
                    ObjectId = flightId,
                    Date = DateTime.Now,
                };

                using (var client = new TcpClient())
                {
                    client.Connect(_ep);
                    Response response;
                    using (var ns = client.GetStream())
                    {
                        ByteTransporting.SendBinary(ns, request);

                        response = (Response)ByteTransporting.GetBinary(ns);
                    }
                    if (response.Message == "PRESENT")
                    {
                        return response.Discount!.Value;
                    }
                    else
                        return 0;
                }
            }
            catch (Exception ex)
            {
                Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            }
            return 0;
        }
    }
}
