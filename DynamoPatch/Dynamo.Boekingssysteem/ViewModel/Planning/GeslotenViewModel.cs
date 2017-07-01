using System;

using Dynamo.Boekingssysteem.ViewModel.Base;
using Dynamo.Common;
using Dynamo.Model;

namespace Dynamo.Boekingssysteem.ViewModel.Planning
{
    public class GeslotenViewModel : EntityViewModel<Gesloten>
    {
        public GeslotenViewModel(Gesloten gesloten)
            : base(gesloten) {}

        public bool Avond
        {
            get { return _entity.Avond; }
            set
            {
                if (value == _entity.Avond)
                    return;

                _entity.Avond = value;

                OnPropertyChanged("Avond");
            }
        }

        public string DatumTot
        {
            get
            {
                return _entity.DatumTot.HasValue
                    ? _entity.DatumTot.Value.GetDynamoDatum()
                    : string.Empty;
            }
            set
            {
                if (_entity.DatumTot.HasValue == false
                    || value != _entity.DatumTot.Value.GetDynamoDatum())
                {
                    if (CommonMethods.IsDynamoDatum(value))
                    {
                        _entity.DatumTot = CommonMethods.DynamoDatum2Datum(value);
                        OnPropertyChanged("DatumTot");
                    }
                }
            }
        }

        public string DatumVan
        {
            get
            {
                if (_entity.DatumVan == DateTime.MinValue)
                {
                    return string.Empty;
                }

                return _entity.DatumVan.GetDynamoDatum();
            }
            set
            {
                if (value == _entity.DatumVan.GetDynamoDatum())
                    return;

                if (CommonMethods.IsDynamoDatum(value))
                {
                    _entity.DatumVan = CommonMethods.DynamoDatum2Datum(value);
                    OnPropertyChanged("DatumVan");
                }
            }
        }

        public bool Dinsdag
        {
            get { return _entity.Dinsdag; }
            set
            {
                if (value == _entity.Dinsdag)
                    return;

                _entity.Dinsdag = value;

                OnPropertyChanged("Dinsdag");
            }
        }

        public bool Donderdag
        {
            get { return _entity.Donderdag; }
            set
            {
                if (value == _entity.Donderdag)
                    return;

                _entity.Donderdag = value;

                OnPropertyChanged("Donderdag");
            }
        }

        public bool Maandag
        {
            get { return _entity.Maandag; }
            set
            {
                if (value == _entity.Maandag)
                    return;

                _entity.Maandag = value;

                OnPropertyChanged("Maandag");
            }
        }

        public bool Middag
        {
            get { return _entity.Middag; }
            set
            {
                if (value == _entity.Middag)
                    return;

                _entity.Middag = value;

                OnPropertyChanged("Middag");
            }
        }

        public bool Oefenruimte1
        {
            get { return _entity.Oefenruimte1; }
            set
            {
                if (value == _entity.Oefenruimte1)
                    return;

                _entity.Oefenruimte1 = value;

                OnPropertyChanged("Oefenruimte1");
            }
        }

        public bool Oefenruimte2
        {
            get { return _entity.Oefenruimte2; }
            set
            {
                if (value == _entity.Oefenruimte2)
                    return;

                _entity.Oefenruimte2 = value;

                OnPropertyChanged("Oefenruimte2");
            }
        }

        public bool Oefenruimte3
        {
            get { return _entity.Oefenruimte3; }
            set
            {
                if (value == _entity.Oefenruimte3)
                    return;

                _entity.Oefenruimte3 = value;

                OnPropertyChanged("Oefenruimte3");
            }
        }

        public string Reden
        {
            get { return _entity.Reden; }
            set
            {
                if (value == _entity.Reden)
                    return;

                _entity.Reden = value;

                OnPropertyChanged("Reden");
            }
        }

        public bool Vrijdag
        {
            get { return _entity.Vrijdag; }
            set
            {
                if (value == _entity.Vrijdag)
                    return;

                _entity.Vrijdag = value;

                OnPropertyChanged("Vrijdag");
            }
        }

        public bool Woensdag
        {
            get { return _entity.Woensdag; }
            set
            {
                if (value == _entity.Woensdag)
                    return;

                _entity.Woensdag = value;

                OnPropertyChanged("Woensdag");
            }
        }

        public bool Zaterdag
        {
            get { return _entity.Zaterdag; }
            set
            {
                if (value == _entity.Zaterdag)
                    return;

                _entity.Zaterdag = value;

                OnPropertyChanged("Zaterdag");
            }
        }

        public bool Zondag
        {
            get { return _entity.Zondag; }
            set
            {
                if (value == _entity.Zondag)
                    return;

                _entity.Zondag = value;

                OnPropertyChanged("Zondag");
            }
        }
    }
}