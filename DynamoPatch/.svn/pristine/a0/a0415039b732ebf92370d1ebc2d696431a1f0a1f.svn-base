using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dynamo.BoekingsSysteem;
using Dynamo.Common;
using Dynamo.BoekingsSysteem.Base;
using System.Windows.Input;

namespace Dynamo.Boekingssysteem.ViewModel.Planning
{
    public class DatumViewModel : ViewModelBase
    {
        private DateTime _datum = DateTime.Today;
        private CommandViewModel _logboek;
        public event EventHandler<ShowLogboekEventArgs> OnShowLogboek;
        public DatumViewModel(DateTime datum)
        {
            _datum = datum;
        }

        public int DikteRand
        {
            get 
            {
                return _datum.Date == DateTime.Today ? 2 : 0;
            }
        }

        public CommandViewModel Logboek
        {
            get
            {
                if (_logboek == null)
                {
                    _logboek = new CommandViewModel("<", new RelayCommand(param => this.ShowLogboek()));
                }
                return _logboek;
            }
        }

        private void ShowLogboek()
        {
            if (OnShowLogboek != null)
            {
                OnShowLogboek(this, new ShowLogboekEventArgs { Datum = _datum});
            }
        }

        public string DagVanDeWeek
        {
            get { return _datum.DagVanDeWeekVoluit(); }
        }

        public string Datum
        {
            get { return _datum.GetDynamoDatum(); }
        }

        

        public void WeekVerder()
        {
            _datum.AddDays(7);
        }

        public void WeekTerug()
        {
            _datum.AddDays(-7);
        }
    }

    public class ShowLogboekEventArgs : EventArgs
    {
        public DateTime Datum;
    }
}
