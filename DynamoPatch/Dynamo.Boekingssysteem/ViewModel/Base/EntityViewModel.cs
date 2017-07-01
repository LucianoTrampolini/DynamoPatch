using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dynamo.Model.Base;
using Dynamo.BoekingsSysteem;
using Dynamo.BL;
using System.Windows.Input;
using Dynamo.Common;

namespace Dynamo.Boekingssysteem.ViewModel.Base
{
    public abstract class EntityViewModel<T> : SubItemViewModel<T>
        where T : ModelBase
    {
        private bool _isSelected = false;
        
        private RelayCommand _defaultCommand;

        public EventHandler DoubleClicked;

        public ICommand DefaultCommand
        {
            get
            {
                if (_defaultCommand == null)
                    _defaultCommand = new RelayCommand(param => OnDefaultCommand());

                return _defaultCommand;
            }
        }

        private void OnDefaultCommand()
        {
            if (DoubleClicked != null)
            {
                DoubleClicked(this, EventArgs.Empty);
            }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value == _isSelected)
                    return;

                _isSelected = value;

                base.OnPropertyChanged("IsSelected");
            }
        }

        
        protected T _entity;

        public EntityViewModel(T entity)
        {
            _entity = entity;
        }

        public T GetEntity()
        {
            return _entity;
        }

        public int Id
        {
            get { return _entity.Id; }
        }

        public bool Verwijderd
        {
            get 
            {
                return _entity.Verwijderd;
            }
            set
            {
                _entity.Verwijderd = value;
            }
        }

        
    }
}
