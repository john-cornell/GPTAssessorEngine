using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPTEngine.Roles
{
    public class Role : IRole
    {
        private readonly RoleBehaviour _roleBehaviour;

        public Role(RoleType roleType, RoleBehaviour roleBehaviour)
        {
            RoleType = roleType;
            _roleBehaviour = roleBehaviour;
        }

        public string Content => "You are configured to respond like this, it is VERY IMPORTANT YOU DO SO!!: " + _roleBehaviour.Content;
        public string Name => _roleBehaviour.Name;
        public ContentType ContentType => _roleBehaviour.ContentType;

        public RoleType RoleType { get; }

        public GPTMessage GetSetupMessage() => new GPTMessage(this.RoleType.ToString().ToLowerInvariant(), Content);
    }
}
