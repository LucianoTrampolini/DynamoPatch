using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dynamo.Boekingssysteem.ViewModel.Band
{
    public class NieuweRekeningViewModel:NieuweBetalingViewModel
    {
        public new decimal Betaald
        {
            get
            {
                return _entity.Bedrag;
            }
            set
            {
                if (_entity.Bedrag == value)
                    return;

                _entity.Bedrag = value;

                OnPropertyChanged("Betaald");
            }
        }

        public NieuweRekeningViewModel(BandViewModel band)
            : base(band)
        {  }
    }
}
