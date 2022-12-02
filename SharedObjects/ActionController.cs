namespace SharedObjects
{
    public class ActionController
    {
        CommandMove _slot;

        public ActionController()
        {
            _slot = new NoCommand();
        }

        public void SetCommand(CommandMove command)
        {
            _slot = command;
        }

        public void Execute()
        { 
            _slot.Execute();
        }
    }
}
