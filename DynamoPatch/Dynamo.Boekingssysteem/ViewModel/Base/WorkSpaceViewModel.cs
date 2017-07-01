using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

using Dynamo.Common.Properties;

namespace Dynamo.BoekingsSysteem.Base
{
    /// <summary>
    /// This ViewModelBase subclass requests to be removed 
    /// from the UI when its CloseCommand executes.
    /// This class is abstract.
    /// </summary>
    public abstract class WorkspaceViewModel : ViewModelBase
    {
        #region Member fields

        private ReadOnlyCollection<CommandViewModel> _commands;

        #endregion

        #region Constructor

        #endregion // Constructor

        #region CloseCommand

        /// <summary>
        /// Returns the command that, when invoked, attempts
        /// to remove this workspace from the user interface.
        /// </summary>
        public ICommand CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                    _closeCommand = new RelayCommand(param => OnRequestClose());

                return _closeCommand;
            }
        }

        #endregion // CloseCommand

        public ReadOnlyCollection<CommandViewModel> Commands
        {
            get
            {
                if (_commands == null)
                {
                    List<CommandViewModel> cmds = CreateCommands();
                    _commands = new ReadOnlyCollection<CommandViewModel>(cmds);
                }
                return _commands;
            }
        }

        public ICommand DefaultCommand
        {
            get
            {
                if (_defaultCommand == null)
                    _defaultCommand = new RelayCommand(param => OnDefaultCommand());

                return _defaultCommand;
            }
        }

        public virtual void OnDefaultCommand() {}

        public void ResetCommands()
        {
            _commands = null;
            OnPropertyChanged("Commands");
        }

        protected virtual List<CommandViewModel> CreateCommands()
        {
            return new List<CommandViewModel>
            {
                new CommandViewModel(
                    StringResources.ButtonSluiten,
                    CloseCommand)
            };
        }

        #region Fields

        RelayCommand _closeCommand;
        RelayCommand _defaultCommand;

        #endregion // Fields
    }
}