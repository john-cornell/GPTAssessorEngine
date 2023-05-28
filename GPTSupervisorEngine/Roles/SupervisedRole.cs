using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GPTEngine.Roles;

namespace AssessorEngine.Roles
{
    public abstract class SupervisedRole : RoleBehaviour
    {
        public string RoleStatement { get; set; }
    }
}
