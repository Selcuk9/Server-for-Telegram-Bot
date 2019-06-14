using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerAdminka
{
    public class Server
    {
        public void Connecting()
        {
            const string ip = "127.0.0.1";
            const int port = 11000;
            IPEndPoint tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
           // SocketInformation information = new SocketInformation();
            Socket socket = new Socket(AddressFamily.InterNetwork,SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(tcpEndPoint);
            socket.Listen(7);
            while (true)
            {
                Console.WriteLine("Server running");
                var listener = socket.Accept();
                byte[] buffer = new byte[2048];
                int size = 0; // Really receievd bytes
                StringBuilder data = new StringBuilder(); // TODO: Text 
                do
                {
                    size = listener.Receive(buffer);
                    data.Append(Encoding.UTF8.GetString(buffer, 0,size));
                }
                while (listener.Available > 0);
                Console.WriteLine(data.ToString());
                string a = "Сервер получил инфу";
                var message = Encoding.UTF8.GetBytes(a); 
                listener.Send(message);
                listener.Shutdown(SocketShutdown.Both);
                listener.Close();
            }
        }
    }
}
