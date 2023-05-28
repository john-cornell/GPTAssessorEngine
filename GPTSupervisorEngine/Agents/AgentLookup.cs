using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssessorEngine.Roles;
using GPTEngine;
using GPTEngine.Roles;
using GPTSupervisorEngine.Roles;

namespace AssessorEngine.Agents
{
    public class AgentLookup
    {

        public event EventHandler<string> RoleAssigned;

        GPT _gpt;
        List<SupervisedRole> _agents;
        Conversation _roleSummariser;

        public event EventHandler SummarisationCompleted;

        public AgentLookup(GPT gpt)
        {
            _roleSummariser = new Conversation(new RoleSummariser());
            _gpt = gpt;

            _agents = new List<SupervisedRole>();
        }

        private async Task AddAgentAsync(SupervisedRole agent)
        {
            _roleSummariser.AddMessage($"Summarise this: {agent.Content}");

            GPTResponse response = (await _gpt.Call(_roleSummariser));

            agent.RoleStatement = response.Response;

            RoleAssigned?.Invoke(this, response.Response);

            _agents.Add(agent);
        }

        public async Task AddAgentsAsync(params SupervisedRole[] agents)
        {
            foreach (var agent in agents)
            {
                await AddAgentAsync(agent);
            }

            SummarisationCompleted?.Invoke(this, EventArgs.Empty);
        }

        public RoleBehaviour GetAgent(string name)
        {
            return _agents.FirstOrDefault(a => a.Name == name);
        }

        public override string ToString()
        {
            return string.Join(", ", _agents.Select(a => $"   (Agent Name: {a.Name}: Agent Role: {a.Content})   \r\n"));
        }
    }
}
