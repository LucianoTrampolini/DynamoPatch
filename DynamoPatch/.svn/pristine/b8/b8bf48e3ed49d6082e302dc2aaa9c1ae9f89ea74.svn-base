using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dynamo.Boekingssysteem.ViewModel.Base;
using Dynamo.BL;

namespace Dynamo.Boekingssysteem.ViewModel.Band
{
    public class BandViewModel : EntityViewModel<Model.Band>
    {
        public string Naam
        {
            get { return _entity.Naam; }
            set
            {
                if (value == _entity.Naam)
                    return;

                _entity.Naam = value;

                base.OnPropertyChanged("Naam");
            }
        }

        public string Contactpersoon
        {
            get 
            {
                if (_entity.ContactPersonen.Count == 0)
                {
                    return string.Empty;
                }
                return _entity.ContactPersonen.FirstOrDefault().Naam; 
            }
        }

        public string Telefoon
        {
            get
            {
                if (_entity.ContactPersonen.Count == 0)
                {
                    return _entity.Telefoon;
                }
                return _entity.ContactPersonen.FirstOrDefault().Telefoon;
            }
        }

        public int Kasten
        {
            get { return _entity.Kasten; }
            set
            {
                if (value == _entity.Kasten)
                    return;

                _entity.Kasten = value;

                base.OnPropertyChanged("Kasten");
            }
        }

        public string Opmerkingen
        {
            get { return _entity.Opmerkingen; }
            set
            {
                if (value == _entity.Opmerkingen)
                    return;

                _entity.Opmerkingen = value;
                base.OnPropertyChanged("Opmerkingen");
            }
        }

        public bool HeeftVerleden
        {
            get { return _entity.HeeftVerleden; }
            set 
            {
                if (value == _entity.HeeftVerleden)
                    return;

                _entity.HeeftVerleden = value;
                base.OnPropertyChanged("HeeftVerleden");
            }
        }
        public BandViewModel(Model.Band band)
            : base(band)
        { }
        
    }
}
