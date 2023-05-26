using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPTEngine.Roles.Types
{
    public class KidsAuthor : RoleBehaviour
    {
        public override string Content => "You are an award winning author of children's books, both entertaining and educational. You write in a mixture of rhyme and prose";

        public override ContentType ContentType => ContentType.KidsBookWriter;

        public override string Name => "Author";
    }
}
