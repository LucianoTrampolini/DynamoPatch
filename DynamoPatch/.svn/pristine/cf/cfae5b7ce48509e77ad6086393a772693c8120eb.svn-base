using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Diagnostics;
using System.Windows;
using Dynamo.Boekingssysteem.ViewModel.Base;
using System.Collections.ObjectModel;
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
        private ReadOnlyCollection<CommandViewModel> _commands;

        public ReadOnlyCollection<CommandViewModel> Commands
        {
            get
            {
                if (_commands == null)
                {
                    List<CommandViewModel> cmds = this.CreateCommands();
                    _commands = new ReadOnlyCollection<CommandViewModel>(cmds);
                }
                return _commands;
            }
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

        public void ResetCommands()
        {
            _commands = null;
            OnPropertyChanged("Commands");
        }

        #region Fields

        RelayCommand _closeCommand;
        RelayCommand _defaultCommand;

        #endregion // Fields

        #region Constructor

        protected WorkspaceViewModel()
        {
        }

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
                    _closeCommand = new RelayCommand(param => this.OnRequestClose());

                return _closeCommand;
            }
        }

        #endregion // CloseCommand

        public ICommand DefaultCommand
        {
            get
            {
                if (_defaultCommand == null)
                    _defaultCommand = new RelayCommand(param => OnDefaultCommand());

                return _defaultCommand;
            }
        }
        public virtual void OnDefaultCommand()
        { }
    }
}
