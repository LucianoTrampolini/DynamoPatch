using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dynamo.Boekingssysteem.ViewModel.Band
{
    public class WijzigBetalingViewModel : NieuweBetalingViewModel
    {
        public new decimal Betaald
        {
            get
            {
                return _entity.Bedrag > 0 ? _entity.Bedrag : _entity.Bedrag * -1;
            }
            set
            {
                if (_entity.Bedrag > 0)
                {
                    if (_entity.Bedrag == value)
                        return;

                    _entity.Bedrag = value;
                }
                else
                {
                    if (_entity.Bedrag == value * -1)
                        return;

                    _entity.Bedrag = value * -1;
                }
                OnPropertyChanged("Betaald");
            }
        }

        public WijzigBetalingViewModel(BandViewModel band, BetalingViewModel betaling)
            : base(band, betaling)
        {
            Omschrijving = _entity.Bedrag > 0 ? "Te betalen" : "Betaald";
        }
    }
}
