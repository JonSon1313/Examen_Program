using Azure.Core;
using DataTransporting;
using DBLayer;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.Methods
{
    public class AddCommand
    {
        public static void Add<T>(FlyCompanyDBInstance _db, NetworkStream _ns, T _obj)
        {
            var response = new Response();
            
            if(_obj is Aircraft)
            {
                response.Aircrafts = [];
                response.Aircrafts?.Add(_db.AddAircraft(_obj as Aircraft ?? new()));
                response.Message = "ADDED";
                Console.WriteLine($"{(_obj as  Aircraft).TailNumber} Aircraft added ");
            }
            else if(_obj is Airport)
            {

            }
              //окрім Seat
            ByteTransporting.SendBinary(_ns, response); 

        }

        
    }
}
