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
        private RedirectEvent _redirectEvent;
        [NonSerialized]
        private AgentManager _currentAgentManager;


        protected abstract void PrepareToSending();
        protected abstract void ResumeCalculation();
        protected virtual void HandleTravelRequest() {}

        public void Resume(AgentManager agentManager)
        {
            _currentAgentManager = agentManager;
            ResumeCalculation();
        }

        public void TravelTo(string target, int port)
        {
            _redirectEvent = new RedirectEvent { Target = target, Port = port };
            HandleTravelRequest();
        }

        protected void TryToTravel()
        {
            if (_redirectEvent == null) return;
            // zastavit vlakno agenta
            PrepareToSending();
            var agentManager = _currentAgentManager;
            var redirectEvent = _redirectEvent;
            Clear();
            agentManager.SendAgent(this, redirectEvent.Target, redirectEvent.Port);
        }

        private void Clear()
        {
            _redirectEvent = null;
            _currentAgentManager = null;
        }

        private class RedirectEvent
        {
            public string Target { get; set; }
            public int Port { get; set; }
        }
    }
}
