using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Dynamo.Boekingssysteem.ViewModel.Base;
using Dynamo.Common.Properties;
using Dynamo.Common;
using System.Collections.ObjectModel;
using Dynamo.BoekingsSysteem.Base;
using Dynamo.BoekingsSysteem;
using System.Windows;
using Dynamo.BL;
using Dynamo.BL.Repository;

namespace Dynamo.Boekingssysteem.ViewModel.Band
{
    public class BandBetalenViewModel : SubItemViewModel< Model.Band>
    {
        private BandViewModel _band;

        public ObservableCollection<BetalingViewModel> Betalingen { get; private set; }

        public string Openstaand { get; private set; }

        public BandBetalenViewModel(BandViewModel band)
        {
            if (band == null)
            {
                throw new ArgumentNullException("band");
            }

            _band = band;

            CreateBetalingenCollection();

            this.DisplayName = string.Format("{0} {1}", StringResources.Betalingen, _band.Naam);
        }

        protected override List<CommandViewModel> CreateCommands()
        {
            return new List<CommandViewModel>
            {
                new CommandViewModel(
                    StringResources.ButtonBetalen,
                    new RelayCommand(param => this.Betalen())),
                new CommandViewModel(
                    StringResources.ButtonNieuw,
                    new RelayCommand(param => this.NieuweRekening())),
                new CommandViewModel(
                    StringResources.ButtonWijzigen,
                    new RelayCommand(param => this.WijzigRekening())),
                new CommandViewModel(
                    StringResources.ButtonVerwijderen,
                    new RelayCommand(param => this.Verwijderen())),
                new CommandViewModel(
                    StringResources.ButtonSluiten,
                    CloseCommand)
            };
        }

        private void WijzigRekening()
        {
            var betalingViewModel = Betalingen.Where(x => x.IsSelected).FirstOrDefault();
            if (betalingViewModel != null)
            {
                SwitchViewModel(new WijzigBetalingViewModel(_band, betalingViewModel));
            }
        }

        private void NieuweRekening()
        {
            SwitchViewModel(new NieuweRekeningViewModel(_band));
        }

        private void Betalen()
        {
            SwitchViewModel(new NieuweBetalingViewModel(_band));
        }

        private void Verwijderen()
        {
            var betalingViewModel = Betalingen.Where(x => x.IsSelected).FirstOrDefault();
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

        private void CreateBetalingenCollection()
        {
            using (var repo = new BetalingRepository())
            {
                List<BetalingViewModel> all = (from betaling in repo.Load(x =>x.BandId==_band.Id && x.Verwijderd == false).OrderByDescending(b=>b.Datum) select new BetalingViewModel(betaling)).ToList();

                this.Betalingen = new ObservableCollection<BetalingViewModel>(all);
                if (Betalingen.Count > 0)
                {
                    foreach (var item in Betalingen)
                    {
                        item.IsSelected = false;
                    }
                    Betalingen.FirstOrDefault().IsSelected = true;
                }
                this.Openstaand = string.Format("{0} {1}", StringResources.TotaalOpenstaand, Betalingen.Sum(x => x.Bedrag).GetDynamoBedrag());
            }
            OnPropertyChanged("Betalingen");
            OnPropertyChanged("Openstaand");
        }

        protected override void OnSubViewModelClosed()
        {
            CreateBetalingenCollection();
        }
    }
}
