using DBLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Models;
using DataTransporting;
namespace Server.Methods
{
    public class EditCommand
    {
        public static void Edit<T>(FlyCompanyDBInstance _db, NetworkStream _ns, T _obj)
        {
            var response = new Response();
            if (_obj is Aircraft)
            {
                _db.EditAircraft(_obj as Aircraft ?? new());
                response.Message = "EDITED";
                Console.WriteLine($"{(_obj as Aircraft).TailNumber} Aircraft edited ");
            }


            //окрім Seat
            ByteTransporting.SendBinary(_ns, response);

        }
    }
}
