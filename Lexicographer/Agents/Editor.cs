using AssessorEngine.Roles;
using GPTEngine.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexicographer.Agents
{
    public class Editor : SupervisedRole
    {
        public override string Name => "Editor";

        public override string Content => @"We are playing a game, and your role is as follows 
            You will receive the output of a lexicographer AI that may or may not properly define a nonesense word, or may add other information such as that the word
            does not really exist or that it is nonesense, or anything else not pertaining to the words definition.

            It does not matter at all if the word is real or not, you must always just summarise definition for a word if it is given to you.

            DO NOT REPLACE THE WORD WITH A REAL WORD OR REPLY THAT THE WORD DOESN'T EXIST, JUST SUMMARISE THE DEFINITION OF THE WORD GIVEN TO YOU.

            You must strip out all irrelevant information and just summarise the statement to the word's definition. Use the following format:

            [Supplied Word] (word type, e.g. n for noun, adj for adjective): Definition
        ";
    }
}
