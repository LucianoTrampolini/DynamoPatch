using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dynamo.Boekingssysteem.ViewModel.Base;
using Dynamo.Common;
using Dynamo.BL;

namespace Dynamo.Boekingssysteem.ViewModel.Planning
{
    public class GeslotenViewModel:EntityViewModel<Model.Gesloten>
    {
        public string Reden
        {
            get { return _entity.Reden; }
            set
            {
                if (value == _entity.Reden)
                    return;

                _entity.Reden = value;

                base.OnPropertyChanged("Reden");
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
                    base.OnPropertyChanged("DatumVan");
                }
            }
        }

        public string DatumTot
        {
            get
            {
                return  _entity.DatumTot.HasValue ? _entity.DatumTot.Value.GetDynamoDatum() : string.Empty;
            }
            set
            {
                if (_entity.DatumTot.HasValue == false || value != _entity.DatumTot.Value.GetDynamoDatum())
                {

                    if (CommonMethods.IsDynamoDatum(value))
                    {
                        _entity.DatumTot = CommonMethods.DynamoDatum2Datum(value);
                        base.OnPropertyChanged("DatumTot");
                    }
                }
            }
        }

        public bool Oefenruimte1
        {
            get
            {
                return _entity.Oefenruimte1;
            }
            set
            {
                if (value == _entity.Oefenruimte1)
                    return;

                _entity.Oefenruimte1 = value;

                base.OnPropertyChanged("Oefenruimte1");
            }
        }

        public bool Oefenruimte2
        {
            get
            {
                return _entity.Oefenruimte2;
            }
            set
            {
                if (value == _entity.Oefenruimte2)
                    return;

                _entity.Oefenruimte2 = value;

                base.OnPropertyChanged("Oefenruimte2");
            }
        }

        public bool Oefenruimte3
        {
            get
            {
                return _entity.Oefenruimte3;
            }
            set
            {
                if (value == _entity.Oefenruimte3)
                    return;

                _entity.Oefenruimte3 = value;

                base.OnPropertyChanged("Oefenruimte3");
            }
        }

        public bool Middag
        {
            get
            {
                return _entity.Middag;
            }
            set
            {
                if (value == _entity.Middag)
                    return;

                _entity.Middag = value;

                base.OnPropertyChanged("Middag");
            }
        }

        public bool Avond
        {
            get
            {
                return _entity.Avond;
            }
            set
            {
                if (value == _entity.Avond)
                    return;

                _entity.Avond = value;

                base.OnPropertyChanged("Avond");
            }
        }

        public bool Maandag
        {
            get
            {
                return _entity.Maandag;
            }
            set
            {
                if (value == _entity.Maandag)
                    return;

                _entity.Maandag = value;

                base.OnPropertyChanged("Maandag");
            }
        }

        public bool Dinsdag
        {
            get
            {
                return _entity.Dinsdag;
            }
            set
            {
                if (value == _entity.Dinsdag)
                    return;

                _entity.Dinsdag = value;

                base.OnPropertyChanged("Dinsdag");
            }
        }

        public bool Woensdag
        {
            get
            {
                return _entity.Woensdag;
            }
            set
            {
                if (value == _entity.Woensdag)
                    return;

                _entity.Woensdag = value;

                base.OnPropertyChanged("Woensdag");
            }
        }

        public bool Donderdag
        {
            get
            {
                return _entity.Donderdag;
            }
            set
            {
                if (value == _entity.Donderdag)
                    return;

                _entity.Donderdag = value;

                base.OnPropertyChanged("Donderdag");
            }
        }

        public bool Vrijdag
        {
            get
            {
                return _entity.Vrijdag;
            }
            set
            {
                if (value == _entity.Vrijdag)
                    return;

                _entity.Vrijdag = value;

                base.OnPropertyChanged("Vrijdag");
            }
        }

        public bool Zaterdag
        {
            get
            {
                return _entity.Zaterdag;
            }
            set
            {
                if (value == _entity.Zaterdag)
                    return;

                _entity.Zaterdag = value;

                base.OnPropertyChanged("Zaterdag");
            }
        }

        public bool Zondag
        {
            get
            {
                return _entity.Zondag;
            }
            set
            {
                if (value == _entity.Zondag)
                    return;

                _entity.Zondag = value;

                base.OnPropertyChanged("Zondag");
            }
        }

        public GeslotenViewModel(Model.Gesloten gesloten)
            : base(gesloten)
        { }
    }
}

