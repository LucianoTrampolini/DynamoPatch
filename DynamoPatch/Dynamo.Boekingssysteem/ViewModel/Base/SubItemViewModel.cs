using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

using Dynamo.BoekingsSysteem;
using Dynamo.BoekingsSysteem.Base;
using Dynamo.Common.Properties;
using Dynamo.Model.Base;

namespace Dynamo.Boekingssysteem.ViewModel.Base
{
    /// <summary>
    /// This ViewModelBase subclass requests to be removed 
    /// from the UI when its CloseCommand executes.
    /// This class is abstract.
    /// </summary>
    public abstract class SubItemViewModel<E> : ViewModelBase
        where E : ModelBase
    {
        #region Member fields

        private RelayCommand _closeCommand;
        private ReadOnlyCollection<CommandViewModel> _commands;

        #endregion

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

        public bool InitialFocus
        {
            get { return true; }
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
    }
}