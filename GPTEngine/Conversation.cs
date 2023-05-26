using GPTEngine.Roles;

namespace GPTEngine
{
    public class Conversation
    {
        List<GPTMessage> _messages;
        bool _resetEachMessage;
        Role _system;
        Role _assistant;
        public Conversation(Role system, Role assistant, bool resetEachMessage)
        {
            _resetEachMessage = resetEachMessage;

            _system = system;
            _assistant = assistant;

            if (system.RoleType != RoleType.System)
            {
                throw new ArgumentException("The first role must be of type System.", nameof(system));
            }

            if (assistant.RoleType != RoleType.Assistant)
            {
                throw new ArgumentException("The second role must be of type Assistant.", nameof(assistant));
            }

            ResetMessages();
        }

        private void ResetMessages()
        {
            _messages = new List<GPTMessage>() { _system.GetSetupMessage(), _assistant.GetSetupMessage() };
        }

        public void AddMessage(string message)
        {
            if (_resetEachMessage) ResetMessages();

            _messages.Add(new GPTMessage("user", message));
        }

        public void AddReply(string message)
        {
            _messages.Add(new GPTMessage("assistant", message));
        }

        public GPTMessage[] Data => _messages.ToArray();
    }

}
