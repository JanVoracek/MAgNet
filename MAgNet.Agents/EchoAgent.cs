using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using MAgNet.Core;

namespace MAgNet.Agents
{
    [Serializable]
    public class EchoAgent : Agent
    {
        private readonly string _name;

        public EchoAgent(string name)
        {
            _name = name;
        }

        protected override void ResumeCalculation()
        {
            Console.WriteLine("Hi there! I am " + _name + "!");
        }

        protected override void HandleTravelRequest()
        {
            Console.WriteLine("A jedeeeem...");
            Travel();
        }
    }

    [Serializable]
    public class LoopAgent : Agent
    {
        private readonly int _loops;
        private bool _done;
        private int _i;
        public int Sum { get; private set; }

        public LoopAgent(int loops)
        {
            _loops = loops;
        }

        protected override void ResumeCalculation()
        {
            for (; _i < _loops; _i++)
            {
                Sum += _i;
                Thread.Sleep(TimeSpan.FromMilliseconds(1));
                if (ShouldTravel())
                {
                    Console.WriteLine("Ending loop " + this);
                    break;
                }
            }
            if(ShouldTravel()) Travel();
            _done = true;
        }

        protected override void HandleTravelRequest()
        {
            if(_done)
                Travel();
        }

        public override string ToString()
        {
            return "LoopAgent: " + Sum;
        }
    }

    [Serializable]
    public class BoomerangAgent : Agent
    {
        private readonly string _originIp;
        private readonly int _originPort;
        private readonly string _targetIp;
        private readonly int _targetPort;
        private int _numberOfResuming;

        public BoomerangAgent(string originIp, int originPort, string targetIp, int targetPort)
        {
            _originIp = originIp;
            _originPort = originPort;
            _targetIp = targetIp;
            _targetPort = targetPort;
        }

        protected override void ResumeCalculation()
        {
            _numberOfResuming += 1;

            if (_numberOfResuming%2 == 1)
            {
                Console.WriteLine("Tak jdem na to...");
                Thread.Sleep(1000);
                CurrentAgentManager.PlanTravel(this, _targetIp, _targetPort);
            }
            else
            {
                Console.WriteLine("A letim zpatky...");
                Thread.Sleep(1000);
                CurrentAgentManager.PlanTravel(this, _originIp, _originPort);
            }
        }

        protected override void HandleTravelRequest()
        {
            Console.WriteLine("Letiiiiim...");
            Travel();
        }
    }
}