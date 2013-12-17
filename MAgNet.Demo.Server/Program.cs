using System;
using System.Linq;
using System.Threading;
using MAgNet.Agents;
using MAgNet.Core;

namespace MAgNet.Demo.Server
{

    class Foo
    {
        public void Run()
        {
            Console.WriteLine("foo");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var foo = new Foo();
            foo.Run();
            RunInSandbox(foo.Run);

            return;
            if (args.Length == 0) args = new[] {"localhost", "3000", "localhost", "3000"};

            var localIp = args[0];
            var localPort = int.Parse(args[1]);

            var agentServer = new AgentServer(localPort);
            var agentManager = new AgentManager(agentServer);


            if (args.Length == 4)
            {
                var targetIp = args[2];
                var targetPort = int.Parse(args[3]);
                var agent = new BoomerangAgent(localIp, localPort, targetIp, targetPort);
                agentManager.Register(agent);
            }

        }

        private static void RunInSandbox(Action action)
        {
            action();
        }
    }
}
