using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GPTEngine.Roles;

namespace Lexicographer.Agents
{
    public class AgentLookup
    { 
        List<RoleBehaviour> _agents;

        public AgentLookup()
        {
            _agents = new List<RoleBehaviour>();
        }

        public void AddAgent(RoleBehaviour agent)
        {
            _agents.Add(agent);
        }

        public void AddAgents(IEnumerable<RoleBehaviour> agents)
        {
            _agents.AddRange(agents);
        }

        public RoleBehaviour GetAgent(string name)
        {
            return _agents.FirstOrDefault(a => a.Name == name);
        }

        public override string ToString()
        {
            return string.Join(", ", _agents.Select(a => $"{a.Name}: {a.Content}"));
        }
    }
}
