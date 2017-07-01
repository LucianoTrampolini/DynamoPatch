﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dynamo.Boekingssysteem.ViewModel.Base;
using Dynamo.Common;
using Dynamo.BL;

namespace Dynamo.Boekingssysteem.ViewModel.Bericht
{
    public class BerichtViewModel : EntityViewModel<Model.Bericht>
    {
        public BerichtViewModel(Model.Bericht bericht)
            : base(bericht)
        { }

        public string Datum
        {
            get { return _entity.Datum.GetDynamoDatum(); }
        }

        public string Titel
        {
            get { return _entity.Titel; }
            set { _entity.Titel = value; }
        }

        public string AangemaaktDoor
        {
            get { return _entity.AangemaaktDoor.Naam; }
        }

        public string Tekst
        {
            get { return _entity.Tekst; }
            set { _entity.Tekst = value; }
        }

        public bool IsBold
        {
            get { return _entity.BeheerderBerichten.Any(x => x.BeheerderId == Helper.CurrentBeheerder.Id && x.Gelezen == null); }
        }
    }
}
