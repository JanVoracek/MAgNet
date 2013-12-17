using System;
using System.Diagnostics.CodeAnalysis;
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
}