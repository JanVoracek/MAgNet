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

            var agent = new LoopAgent(1000);
            var target = args[1];
            var port = int.Parse(args[2]);

            Console.WriteLine("before register " + agent);
            agentManager.Register(agent);
            Console.WriteLine("after register " + agent);
            Thread.Sleep(TimeSpan.FromMilliseconds(100));
            agentManager.PlanTravel(agent, target, port);
            Thread.Sleep(TimeSpan.FromMilliseconds(1000));
            agentManager.Register(agent);
            Thread.Sleep(TimeSpan.FromMilliseconds(5000));
            Console.WriteLine(agent.ToString());
            Console.WriteLine(agentManager.Agents.FirstOrDefault());
        }
    }
}
