using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MAgNet.Core
{
    public class AgentManager
    {
        private readonly AgentServer _agentServer;

        public AgentManager(AgentServer agentServer)
        {
            _agentServer = agentServer;
            agentServer.Listen(Register);
        }

        public IEnumerable<Agent> Agents
        {
            get { return _agents; }
        }

        private readonly List<Agent> _agents = new List<Agent>();

        public void Register(Agent agent)
        {
            _agents.Add(agent);
            new Thread(() => agent.Resume(this)).Start();
        }

        public void SendAgent(Agent agent, string target, int port)
        {
            _agentServer.Send(target, port, agent);
        }
    }
}
