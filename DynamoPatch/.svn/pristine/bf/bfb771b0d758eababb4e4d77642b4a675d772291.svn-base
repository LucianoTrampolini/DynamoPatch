using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dynamo.BoekingsSysteem;
using Dynamo.Common;
using Dynamo.Boekingssysteem.ViewModel.Base;

namespace Dynamo.Boekingssysteem.ViewModel.Beheerder
{
    /// <summary>
    /// Class puur om de vergoedingen te kunnen tonen
    /// </summary>
    public class VergoedingViewModel :  EntityViewModel<Model.Vergoeding>
    {
        public string Datum
        {
            get { return _entity.Datum.GetDynamoDatum(); }
        }

        public string Dag
        {
            get { return _entity.Datum.DagVanDeWeekVoluit(); }
        }

        public string Dagdeel
        {
            get { return _entity.Dagdeel.Omschrijving; }
        }

        public string Taak
        {
            get { return _entity.Taak.Omschrijving; }
        }

        public string Bedrag
        {
            get { return _entity.Bedrag.GetDynamoBedrag(); }
        }

        public VergoedingViewModel(Model.Vergoeding vergoeding):base(vergoeding)
        {
            if (vergoeding == null)
            {
                throw new ArgumentNullException("vergoeding");
            }
        }
    }
}
