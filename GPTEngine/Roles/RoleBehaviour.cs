using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPTEngine.Roles
{
    public abstract class RoleBehaviour
    {
        public abstract string Name { get; }
        public abstract string Content { get; }
        public abstract ContentType ContentType { get; }

        public Role As(RoleType roleType)
        {
            return new Role(roleType, this);
        }

        public virtual bool ResetEachTime => false;
    }
}
