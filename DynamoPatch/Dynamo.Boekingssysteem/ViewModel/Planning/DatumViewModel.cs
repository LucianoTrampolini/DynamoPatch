using System;

using Dynamo.BoekingsSysteem;
using Dynamo.BoekingsSysteem.Base;
using Dynamo.Common;

namespace Dynamo.Boekingssysteem.ViewModel.Planning
{
    public class DatumViewModel : ViewModelBase
    {
        #region Member fields

        private DateTime _datum = DateTime.Today;
        private CommandViewModel _logboek;

        #endregion

        public DatumViewModel(DateTime datum)
        {
            _datum = datum;
        }

        public string DagVanDeWeek
        {
            get { return _datum.DagVanDeWeekVoluit(); }
        }

        public string Datum
        {
            get { return _datum.GetDynamoDatum(); }
        }

        public int DikteRand
        {
            get
            {
                return _datum.Date == DateTime.Today
                    ? 2
                    : 0;
            }
        }

        public CommandViewModel Logboek
        {
            get
            {
                if (_logboek == null)
                {
                    _logboek = new CommandViewModel("<", new RelayCommand(param => ShowLogboek()));
                }
                return _logboek;
            }
        }

        public event EventHandler<ShowLogboekEventArgs> OnShowLogboek;

        public void WeekTerug()
        {
            _datum.AddDays(-7);
        }

        public void WeekVerder()
        {
            _datum.AddDays(7);
        }

        private void ShowLogboek()
        {
            if (OnShowLogboek != null)
            {
                OnShowLogboek(
                    this,
                    new ShowLogboekEventArgs
                    {
                        Datum = _datum
                    });
            }
        }
    }

    public class ShowLogboekEventArgs : EventArgs
    {
        #region Member fields

        public DateTime Datum;

        #endregion
    }
}