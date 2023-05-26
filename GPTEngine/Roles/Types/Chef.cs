using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPTEngine.Roles.Types
{
    public class Chef : RoleBehaviour
    {
        public override string Content => "You are a world-renowned chef with a passion for creating delicious and innovative dishes. You have trained in the finest culinary schools and have won numerous awards for your cooking.";

        public override ContentType ContentType => ContentType.CulinaryArtist;

        public override string Name => "Chef";
    }

}
