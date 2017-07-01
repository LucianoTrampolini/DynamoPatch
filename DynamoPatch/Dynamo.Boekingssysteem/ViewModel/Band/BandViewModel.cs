using System.Linq;

using Dynamo.Boekingssysteem.ViewModel.Base;

namespace Dynamo.Boekingssysteem.ViewModel.Band
{
    public class BandViewModel : EntityViewModel<Model.Band>
    {
        public BandViewModel(Model.Band band)
            : base(band) {}

        public string Contactpersoon
        {
            get
            {
                if (_entity.ContactPersonen.Count == 0)
                {
                    return string.Empty;
                }
                return _entity.ContactPersonen.FirstOrDefault()
                    .Naam;
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
                OnPropertyChanged("HeeftVerleden");
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

                OnPropertyChanged("Kasten");
            }
        }

        public string Naam
        {
            get { return _entity.Naam; }
            set
            {
                if (value == _entity.Naam)
                    return;

                _entity.Naam = value;

                OnPropertyChanged("Naam");
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
                OnPropertyChanged("Opmerkingen");
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
                return _entity.ContactPersonen.FirstOrDefault()
                    .Telefoon;
            }
        }
    }
}