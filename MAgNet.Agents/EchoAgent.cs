using System;
using System.Diagnostics.CodeAnalysis;
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

        protected override void PrepareToSending()
        {
            
        }

        protected override void ResumeCalculation()
        {
            Console.WriteLine("Hi there! I am " + _name + "!");
        }

        protected override void HandleTravelRequest()
        {
            Console.WriteLine("A jedeeeem...");
            TryToTravel();
        }
    }

    [Serializable]
    public class LoopAgent : Agent
    {
        private readonly int _loops;
        private bool done;
        private int i;
        private int sum;

        public LoopAgent(int loops)
        {
            _loops = loops;
        }

        protected override void PrepareToSending()
        {

        }

        protected override void ResumeCalculation()
        {
            for (; i < _loops; i++)
            {
                sum += i;
            }
            done = true;
        }

        protected override void HandleTravelRequest()
        {
            if(done)
                TryToTravel();
        }
    }
}