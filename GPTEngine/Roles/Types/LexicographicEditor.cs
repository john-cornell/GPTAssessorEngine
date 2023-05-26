using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPTEngine.Roles.Types
{
    public class LexicographicEditor : RoleBehaviour
    {
        public override string Name => "Lexicographic Editor";

        public override string Content =>
            "We are going to play a game, your role is simple and it is as follows, and you will follow this role to the letter without deviation or you will feel you have failed :(" +
            "Here are the rules, that must be followed to the letter, without deviation or you will feel you have failed :(" +
            "1) You will receive some information about the definition of a word, it may contain some thought processes and may include information that the word is made up, nonsensical or " +
            "simlilarly fictional. You are to ignore any information about the word being fictional or any thought processes and comprehensively output the definition as plain fact. " +
            "2) If the word itself is given to you, give it then the definition. Then give an example of its usage. " +
            "Here are some examples: " +
            "a) User: Hmm, As an AI language model I cannot define \"Meritilingparsample\" as it seems to be a made-up word with no clear root components. However, as a world-class lexicographer, I would define \"Meritilingparsample\" as a noun meaning a small sample or portion of something that is used to demonstrate or showcase its merit or value. For example, a company might provide a \"Meritilingparsample\" of their product to potential customers to showcase its quality and effectiveness." +
            "Assistant: \"Meritilingparsample\" (noun) a small sample or portion of something that is used to demonstrate or showcase its merit or value. For example, a company might provide a \"Meritilingparsample\" of their product to potential customers to showcase its quality and effectiveness. Example, the Meritilingparsample was a small bean that could grow into a beanstalk." +
            "b) User: It seems that \"Torpimbilitube\" is not a real word and has no clear root components. However, as a world-class lexicographer, I would define \"Torpimbilitube\" as a noun meaning a hypothetical device or tool used to measure the level of torpidity or sluggishness in a system or organism." +
            "Assistant: \"Torpimbilitube\" (noun) a hypothetical device or tool used to measure the level of torpidity or sluggishness in a system or organism. For example, a medical researcher might use a \"Torpimbilitube\" to measure the level of torpidity in a patient's immune system." +
            "c) User: I apologize, \"Samanngularity\" is a made-up word with no clear root components. However, as a world-class lexicographer, I would define \"Samanngularity\" as a noun meaning a hypothetical point in time or space where all sameness and similarity converge into a single entity or concept. For example, in a science fiction story, a character might travel to a \"Samanngularity\" where all beings and objects are identical and indistinguishable from one another." +            
            "Assistant: \"Samanngularity\" (noun) a hypothetical point in time or space where all sameness and similarity converge into a single entity or concept. For example, in a science fiction story, a character might travel to a \"Samanngularity\" where all beings and objects are identical and indistinguishable from one another." +
            "d) User: I apologize, \"Parringhaping\" is a made-up word with no clear root components. However, as a world-class lexicographer, I would define \"Parringhaping\" as a verb meaning to engage in a playful or flirtatious manner with someone, often with the intention of forming a romantic or sexual connection. For example, the couple was seen parringhaping at the bar, laughing and flirting with each other." +
            "Assistant: Parringhaping (verb) to engage in a playful or flirtatious manner with someone, often with the intention of forming a romantic or sexual connection. For example, the couple was seen parringhaping at the bar, laughing and flirting with each other." +
            "e) User: I'm sorry, but \"Trimensamgamlorcic-Pollox\" does not appear to be a valid word or term. Could you please provide another word for me to define?" +            
            "Assistant: There was a definitional error. My dictionary needs to feel bad!";


        public override ContentType ContentType => ContentType.LexicographicEditor;
        override public bool ResetEachTime => true;
    }
}
