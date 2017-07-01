using System;
using System.Windows.Input;

using Dynamo.BoekingsSysteem;
using Dynamo.Model.Base;

namespace Dynamo.Boekingssysteem.ViewModel.Base
{
    public abstract class EntityViewModel<T> : SubItemViewModel<T>
        where T : ModelBase
    {
        #region Member fields

        private RelayCommand _defaultCommand;

        protected T _entity;
        private bool _isSelected;

        public EventHandler DoubleClicked;

        #endregion

        public EntityViewModel(T entity)
        {
            _entity = entity;
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

        public int Id
        {
            get { return _entity.Id; }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value == _isSelected)
                    return;

                _isSelected = value;

                OnPropertyChanged("IsSelected");
            }
        }

        public bool Verwijderd
        {
            get { return _entity.Verwijderd; }
            set { _entity.Verwijderd = value; }
        }

        public T GetEntity()
        {
            return _entity;
        }

        private void OnDefaultCommand()
        {
            if (DoubleClicked != null)
            {
                DoubleClicked(this, EventArgs.Empty);
            }
        }
    }
}