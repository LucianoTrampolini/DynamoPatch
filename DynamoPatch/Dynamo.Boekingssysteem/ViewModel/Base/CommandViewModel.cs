using System;
using System.Windows.Input;

namespace Dynamo.BoekingsSysteem.Base
{
    public class CommandViewModel : ViewModelBase
    {
        public CommandViewModel(string displayName, ICommand command)
        {
            if (command == null)
                throw new ArgumentNullException("command");

            DisplayName = displayName;
            Command = command;
        }

        public ICommand Command { get; private set; }
    }
}