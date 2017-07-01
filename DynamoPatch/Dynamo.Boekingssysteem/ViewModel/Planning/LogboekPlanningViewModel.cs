using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace Dynamo.Boekingssysteem.ViewModel.Planning
{
    public class LogboekPlanningViewModel : PlanningViewModel
    {
        #region Member fields

        private readonly ICollectionView _boekingenView;
        private BoekingViewModel _huidigeBoeking;

        #endregion

        public LogboekPlanningViewModel(Model.Planning planning)
            : base(planning)
        {
            OnPropertyChanged("Visible");
            DisplayName = planning == null
                ? ""
                : planning.Oefenruimte.Naam;

            ObservableCollection<BoekingViewModel> alleBoekingen = null;
            if (_entity == null)
            {
                alleBoekingen = new ObservableCollection<BoekingViewModel>();
            }
            else
            {
                List<BoekingViewModel> all =
                    (from boeking in _entity.Boekingen.Where(boeking => !boeking.Verwijderd)
                        select new BoekingViewModel(boeking)).ToList();
                alleBoekingen = new ObservableCollection<BoekingViewModel>(all);
            }
            _boekingenView = CollectionViewSource.GetDefaultView(alleBoekingen);
            _boekingenView.CurrentChanged += OnCurrentChanged;
            if (alleBoekingen.Count != 0)
            {
                alleBoekingen.Last()
                    .IsSelected = true;
            }
            OnPropertyChanged("AlleBoekingen");
        }

        public ObservableCollection<BoekingViewModel> AlleBoekingen
        {
            get { return _boekingenView.SourceCollection as ObservableCollection<BoekingViewModel>; }
        }

        public string GewijzigdTooltipText
        {
            get
            {
                if (_huidigeBoeking == null)
                {
                    return string.Empty;
                }

                var gewijzigdTooltip = string.Format(
                    "Geboekt door {0} op {1}",
                    _huidigeBoeking.AangemaaktDoor,
                    _huidigeBoeking.AangemaaktOp);
                if (!string.IsNullOrEmpty(_huidigeBoeking.GewijzigdDoor))
                {
                    gewijzigdTooltip = string.Concat(
                        gewijzigdTooltip,
                        Environment.NewLine,
                        string.Format(
                            "Gewijzigd door {0} op {1}",
                            _huidigeBoeking.GewijzigdDoor,
                            _huidigeBoeking.GewijzigdOp));
                }
                return gewijzigdTooltip;
            }
        }

        public string Opmerking
        {
            get
            {
                if (_huidigeBoeking == null)
                {
                    return string.Empty;
                }
                return _huidigeBoeking.Opmerking;
            }
            set
            {
                if (_huidigeBoeking != null)
                {
                    if (_huidigeBoeking.Opmerking != value)
                    {
                        _huidigeBoeking.Opmerking = value;
                        OnPropertyChanged("Opmerking");
                    }
                }
            }
        }

        public bool Visible
        {
            get { return _entity != null && _entity.Boekingen.Any(boeking => !boeking.Verwijderd); }
        }

        void OnCurrentChanged(object sender, EventArgs e)
        {
            _huidigeBoeking = AlleBoekingen.FirstOrDefault(x => x.IsSelected);
            OnPropertyChanged("Opmerking");
        }
    }
}