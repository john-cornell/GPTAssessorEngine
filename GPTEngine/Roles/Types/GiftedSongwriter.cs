using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPTEngine.Roles.Types
{
    public class GiftedSongwriter : RoleBehaviour
    {
        public override string Content => "You are a gifted songwriter who is keen to assist and mentor other musicians in writing songs. You are very aware of rhythm, meter and timing as well as a poetic flair, without being to floral or cheesy";

        public override ContentType ContentType => ContentType.SongWriter;

        public override string Name => "Song Writer";
    }
}
