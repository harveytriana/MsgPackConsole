using MessagePack;
using MML.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

// https://www.strathweb.com/2017/06/using-messagepack-with-asp-net-core-mvc/
//  
// https://github.com/WebApiContrib/WebAPIContrib.Core#formatters

// Source
// https://github.com/neuecc/MessagePack-CSharp/

namespace MsgPackConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("MessagePack Nuget");
            Console.WriteLine("Author: Visionary S.A.S.\n");

            var objectSample = GetOnlineRecord();
            Console.WriteLine($"Object: {objectSample}\n");

            var pack = MessagePackSerializer.Serialize(objectSample);
            Console.WriteLine($"Packed, bytes: {pack.Length}");

            var js = JsonConvert.SerializeObject(objectSample);
            Console.WriteLine($"Pure JSON: {js.Length}");

            var unpack = MessagePackSerializer.Deserialize<OnlineRecord>(pack);
            Console.WriteLine($"Unpacked: {unpack}");

            //* Una alternativa, sin necesidad de usar un formatter
            //* pero requiere serializar y deserializar json
            // var jsArray = JsonConvert.SerializeObject(pack);
            // Console.WriteLine($"\nObject Packed to JSON array:\n{jsArray.Length}");

            Console.Write("\n\nPause...");
            Console.ReadKey();
        }

        private static OnlineRecord GetOnlineRecord()
        {
            var data = new OnlineRecord {
                Time = DateTime.Now,
                Depth = 2042.0F,
                ID = "SAMP-0001",
                Parameters = new List<Parameter>()
            };

            var parametersCount = 120;

            for (int i = 0; i < parametersCount; i++) {
                data.Parameters.Add(new Parameter { Mnemonic = RandomString(4), Value = (float)random.NextDouble() });
            };

            Console.WriteLine("OnlineRecord:");
            Console.WriteLine("Time: {0}", data.Time);
            Console.WriteLine("Depth: {0}", data.Depth);
            data.Parameters.ForEach(p => {
                Console.WriteLine("  {0}", p);
            });
            Console.WriteLine();
            return data;
        }

        static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }


}
