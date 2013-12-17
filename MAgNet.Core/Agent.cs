using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAgNet.Core
{
    [Serializable]
    public abstract class Agent
    {
        [NonSerialized]
        private volatile RedirectEvent _redirectEvent;
        [NonSerialized]
        private volatile AgentManager _currentAgentManager;
        [NonSerialized]
        internal bool Sent;

        protected abstract void ResumeCalculation();
        protected virtual void HandleTravelRequest() {}

        internal void Resume(AgentManager agentManager)
        {
            Console.WriteLine("Resuming agent " + this);
            _currentAgentManager = agentManager;
            ResumeCalculation();
        }

        internal void TravelTo(string target, int port)
        {
            _redirectEvent = new RedirectEvent { Target = target, Port = port };
            HandleTravelRequest();
        }

        protected bool ShouldTravel()
        {
            return _redirectEvent != null;
        }

        protected void Travel()
        {
            if (!ShouldTravel()) return;
            var agentManager = _currentAgentManager;
            var redirectEvent = _redirectEvent;
            Sent = true;
            agentManager.SendAgent(this, redirectEvent.Target, redirectEvent.Port);
        }

        private class RedirectEvent
        {
            public string Target { get; set; }
            public int Port { get; set; }
        }
    }
}
