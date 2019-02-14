using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HL7Sender
{
    class Program
    {
        static void Main(string[] args)
        {

            try

            {

                Console.WriteLine("SEND LOCATION (IP)");

                var LocationIP = Console.ReadLine();

                Console.WriteLine("PORT");

                var LocationPort = Console.ReadLine();

                Console.WriteLine("FILE PATH");

                var FilePath = Console.ReadLine();

                Console.WriteLine("CONNECTING");

                var HL7 = File.ReadAllText(FilePath);

                HL7 = (Char)11 + HL7 + (Char)28 + (Char)13;

                var Message = System.Text.Encoding.ASCII.GetBytes(HL7);

                int Port = int.Parse(LocationPort);

                TcpClient client = new TcpClient(LocationIP, Port);

                NetworkStream Stream = client.GetStream();

                byte[] ACK = new byte[client.ReceiveBufferSize + 1];

                var Count = 0;

                do

                {

                Stream.Write(Message, 0, Message.Length);

                Stream.Read(ACK, 0, ACK.Length);

                Console.WriteLine("");

                Count = Count + 1;

                Console.WriteLine("MESSAGE: " + Count);

                Console.WriteLine("");

                var HL7Clean = Regex.Replace(HL7, @"[^\u0020-\u007E]", string.Empty);

                Console.WriteLine(HL7Clean);

                Console.WriteLine("");

                Console.WriteLine("ACK");

                Console.WriteLine("");

                var ACKClean = Encoding.ASCII.GetString(ACK);

                ACKClean = Regex.Replace(ACKClean, @"[^\u0020-\u007E]", string.Empty);

                Console.WriteLine(ACKClean);

                Console.WriteLine("");

                //Console.ReadLine();
                }

                while (1 != 2);
            }

            catch (Exception ex)

            {

                Console.WriteLine(ex.Message.ToString());

                Main(args);

            }
        }
    }
}
