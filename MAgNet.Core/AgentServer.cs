using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace MAgNet.Core
{
    public delegate void AgentServerListener(Agent agent);

    public class AgentServer
    {
        private AgentServerListener _listener;

        private TcpListener _tcpListener;
        private Thread _listenThread;
        private readonly int _port;

        public AgentServer(int port)
        {
            _port = port;
        }

        public void Listen(AgentServerListener listener)
        {
            _listener = listener;
            _tcpListener = new TcpListener(IPAddress.Any, _port);
            _listenThread = new Thread(ListenForClients);
            _listenThread.Start();
        }

        private void ListenForClients()
        {
            _tcpListener.Start();
            while (true)
            {
                var client = _tcpListener.AcceptTcpClient();
                var clientThread = new Thread(HandleClientComm);
                clientThread.Start(client);
            }
        }

        private void HandleClientComm(object obj)
        {
            var client = (TcpClient) obj;
            var stream = client.GetStream();
            var formatter = new BinaryFormatter();
            var agent = (Agent)formatter.Deserialize(stream);
            Console.WriteLine("Received agent " + agent);
            _listener(agent);
        }

        public void Send(string address, int port, Agent agent)
        {
            Console.WriteLine("sending agent " + agent);
            var stream = new MemoryStream();
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, agent);

            var client = new TcpClient(address, port);
            var ns = client.GetStream();
            Console.WriteLine("Sending agent: " + agent);
            var bytes = stream.ToArray();
            ns.Write(bytes, 0, bytes.Length);
        }
    }
}
