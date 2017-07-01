using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dynamo.BoekingsSysteem;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Dynamo.BoekingsSysteem.Base;
using Dynamo.Common.Properties;

namespace Dynamo.Boekingssysteem.ViewModel.Base
{
    /// <summary>
    /// This ViewModelBase subclass requests to be removed 
    /// from the UI when its CloseCommand executes.
    /// This class is abstract.
    /// </summary>
    public abstract class SubItemViewModel<E> : ViewModelBase where E:Model.Base.ModelBase
    {
        private RelayCommand _closeCommand;
        private ReadOnlyCollection<CommandViewModel> _commands;

        public bool InitialFocus
        {
            get { return true; }
        }

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

        
    }
}
