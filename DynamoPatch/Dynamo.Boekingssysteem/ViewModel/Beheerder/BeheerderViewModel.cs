using System;

using Dynamo.Boekingssysteem.ViewModel.Base;

namespace Dynamo.Boekingssysteem.ViewModel.Beheerder
{
    /// <summary>
    /// Het beheerder viewmodel
    /// </summary>
    public class BeheerderViewModel : EntityViewModel<Model.Beheerder>
    {
        public BeheerderViewModel(Model.Beheerder beheerder) : base(beheerder)
        {
            if (beheerder == null)
            {
                throw new ArgumentNullException("beheerder");
            }
        }

        public string Adres
        {
            get { return _entity.Adres; }
            set
            {
                if (value == _entity.Adres)
                    return;

                _entity.Adres = value;

                OnPropertyChanged("Adres");
            }
        }

        public string Email
        {
            get { return _entity.Email; }
            set
            {
                if (value == _entity.Email)
                    return;

                _entity.Email = value;

                OnPropertyChanged("Email");
            }
        }

        public string Mobiel
        {
            get { return _entity.Mobiel; }
            set
            {
                if (value == _entity.Mobiel)
                    return;

                _entity.Mobiel = value;

                OnPropertyChanged("Mobiel");
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

        public string Plaats
        {
            get { return _entity.Plaats; }
            set
            {
                if (value == _entity.Plaats)
                    return;

                _entity.Plaats = value;

                OnPropertyChanged("Plaats");
            }
        }

        public string Postcode
        {
            get { return _entity.Postcode; }
            set
            {
                if (value == _entity.Postcode)
                    return;

                _entity.Postcode = value;

                OnPropertyChanged("Postcode");
            }
        }

        public string Telefoon
        {
            get { return _entity.Telefoon; }
            set
            {
                if (value == _entity.Telefoon)
                    return;

                _entity.Telefoon = value;

                OnPropertyChanged("Telefoon");
            }
        }

        public override string ToString()
        {
            return _entity == null
                ? "<Error - geen entiteit>"
                : _entity.Naam;
        }
    }
}