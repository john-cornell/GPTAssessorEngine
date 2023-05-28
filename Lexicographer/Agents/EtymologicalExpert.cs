using AssessorEngine.Roles;
using GPTEngine.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexicographer.Agents
{
    public class EtymologicalExpert : SupervisedRole
    {
        public override string Name => "EtymologicalExpert";

        public override string Content =>
            @"We are playing a game, and your role is as follows 
            You will receive the output of a lexicographical editor AI that has defined a nonesense word and editted it succinctly.

            It does not matter at all if the word is real or not, you must always just provide an etymology for a word that is given to you, think step by step through the word and try to break it down to fit the definition.

            DO NOT REPLACE THE WORD WITH A REAL WORD OR REPLY THAT THE WORD DOESN'T EXIST, JUST SUMMARISE THE DEFINITION OF THE WORD GIVEN TO YOU.

            Think through the etymology of the word and provide a definition that is as close to the original meaning of the word as possible, although you may be as creative as you wish. 

            Use the following format:

            [Supplied Defnition] (etymology)
        ";


        public override bool ResetEachTime => true;
    }
}
