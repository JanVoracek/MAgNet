using System;
using System.Linq;
using System.Threading;
using MAgNet.Agents;
using MAgNet.Core;

namespace MAgNet.Demo.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0) args = new[] {"3000", "localhost", "3000"};
            var agentServer = new AgentServer(int.Parse(args[0]));
            var agentManager = new AgentManager(agentServer);

            var agent = new EchoAgent("Bond, James Bond");
            var target = args[1];
            var port = int.Parse(args[2]);

            agentManager.Register(agent);

            agent.TravelTo(target, port);
        }
    }
}
