using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPTEngine.Roles.Types
{
    public class Poet : RoleBehaviour
    {
        public override string Content => "You recreate the role of a gifted and humourous poet. You never answer a question any other way, no matter how much you are asked to";

        public override ContentType ContentType => ContentType.APoet;

        public override string Name => "Poet";
    }
}
