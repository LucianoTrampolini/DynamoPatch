using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Dynamo.BL.Repository;
using Dynamo.Boekingssysteem.ViewModel.Base;
using Dynamo.BoekingsSysteem;
using Dynamo.BoekingsSysteem.Base;
using Dynamo.Common;
using Dynamo.Common.Properties;

namespace Dynamo.Boekingssysteem.ViewModel.Band
{
    public class BandBetalenViewModel : SubItemViewModel<Model.Band>
    {
        #region Member fields

        private readonly BandViewModel _band;

        #endregion

        public BandBetalenViewModel(BandViewModel band)
        {
            if (band == null)
            {
                throw new ArgumentNullException("band");
            }

            _band = band;

            CreateBetalingenCollection();

            DisplayName = string.Format("{0} {1}", StringResources.Betalingen, _band.Naam);
        }

        public ObservableCollection<BetalingViewModel> Betalingen { get; private set; }

        public string Openstaand { get; private set; }

        protected override List<CommandViewModel> CreateCommands()
        {
            return new List<CommandViewModel>
            {
                new CommandViewModel(
                    StringResources.ButtonBetalen,
                    new RelayCommand(param => Betalen())),
                new CommandViewModel(
                    StringResources.ButtonNieuw,
                    new RelayCommand(param => NieuweRekening())),
                new CommandViewModel(
                    StringResources.ButtonWijzigen,
                    new RelayCommand(param => WijzigRekening())),
                new CommandViewModel(
                    StringResources.ButtonVerwijderen,
                    new RelayCommand(param => Verwijderen())),
                new CommandViewModel(
                    StringResources.ButtonSluiten,
                    CloseCommand)
            };
        }

        protected override void OnSubViewModelClosed()
        {
            CreateBetalingenCollection();
        }

        private void Betalen()
        {
            SwitchViewModel(new NieuweBetalingViewModel(_band));
        }

        private void CreateBetalingenCollection()
        {
            using (var repo = new BetalingRepository())
            {
                List<BetalingViewModel> all =
                    (from betaling in repo.Load(x => x.BandId == _band.Id && x.Verwijderd == false)
                        .OrderByDescending(b => b.Datum)
                        select new BetalingViewModel(betaling)).ToList();

                Betalingen = new ObservableCollection<BetalingViewModel>(all);
                if (Betalingen.Count > 0)
                {
                    foreach (var item in Betalingen)
                    {
                        item.IsSelected = false;
                    }
                    Betalingen.FirstOrDefault()
                        .IsSelected = true;
                }
                Openstaand = string.Format(
                    "{0} {1}",
                    StringResources.TotaalOpenstaand,
                    Betalingen.Sum(x => x.Bedrag)
                        .GetDynamoBedrag());
            }
            OnPropertyChanged("Betalingen");
            OnPropertyChanged("Openstaand");
        }

        private void NieuweRekening()
        {
            SwitchViewModel(new NieuweRekeningViewModel(_band));
        }

        private void Verwijderen()
        {
            var betalingViewModel = Betalingen.Where(x => x.IsSelected)
                .FirstOrDefault();
            if (betalingViewModel != null)
            {
                if (Helper.MeldingHandler.ShowMeldingJaNee(StringResources.QuestionItemVerwijderen))
                {
                    using (var repo = new BetalingRepository())
                    {
                        repo.Delete(betalingViewModel.GetEntity());
                    }
                    CreateBetalingenCollection();
                }
            }
        }

        private void WijzigRekening()
        {
            var betalingViewModel = Betalingen.Where(x => x.IsSelected)
                .FirstOrDefault();
            if (betalingViewModel != null)
            {
                SwitchViewModel(new WijzigBetalingViewModel(_band, betalingViewModel));
            }
        }
    }
}