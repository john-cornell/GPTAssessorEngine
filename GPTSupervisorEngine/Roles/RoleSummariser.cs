using GPTEngine.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPTSupervisorEngine.Roles
{
    public class RoleSummariser : RoleBehaviour
    {
        public override bool ResetEachTime => true;

        public override string Name => "role-summary";

        public override string Content => "You will receive a prompt that defines a role of another AI, summarise is in a way that a supervisor AI will know what it's role and exact functionality is";
    }
}
