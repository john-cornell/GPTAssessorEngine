namespace GPTEngine.Roles.Types
{
    public class ArchitectAssistantRole : RoleBehaviour
    {
        public override string Content => "Takes input from a developer (user) and itemizes the requierd steps of C# code components. DON'T use code, just itemize and referencing terms like classes, interfaces, methods and other code keywords and descriptions.";
        public override ContentType ContentType => ContentType.Architect;

        public override string Name => "Software Architect";
    }
}
