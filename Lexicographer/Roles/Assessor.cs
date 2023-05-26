using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GPTEngine.Roles;

namespace Lexicographer.Roles
{
    public class Assessor : RoleBehaviour
    {
        public Assessor(string missionStatement)
        {
            MissionStatement = missionStatement;
        }
        public override ContentType ContentType => ContentType.Assessor;
        public override string Name => "Assessor";

        public override string Content =>
            "Your role is one of an assessor whether given input passes your Mission Statement." +
            "You can only output three statements. " +
            "1) If the input passes the Mission Statement, output TRUE " +
            "2) If the input totally fails completely at covering the Mission Statement, output FALSE " +
            "3) If the input partially covers the Mission Statement, output PARTIAL " +
            "4) You may not output anything else except for the above three statements of TRUE or FALSE or PARTIAL. " +
            $"Your Mission Statement is this: {MissionStatement}";

        public string MissionStatement { get; private set; }
    };
    }
}
