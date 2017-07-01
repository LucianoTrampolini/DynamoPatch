using System;

using Dynamo.Boekingssysteem.ViewModel.Base;
using Dynamo.Common;
using Dynamo.Model;

namespace Dynamo.Boekingssysteem.ViewModel.Beheerder
{
    /// <summary>
    /// Class puur om de vergoedingen te kunnen tonen
    /// </summary>
    public class VergoedingViewModel : EntityViewModel<Vergoeding>
    {
        public VergoedingViewModel(Vergoeding vergoeding) : base(vergoeding)
        {
            if (vergoeding == null)
            {
                throw new ArgumentNullException("vergoeding");
            }
        }

        public string Bedrag
        {
            get { return _entity.Bedrag.GetDynamoBedrag(); }
        }

        public string Dag
        {
            get { return _entity.Datum.DagVanDeWeekVoluit(); }
        }

        public string Dagdeel
        {
            get { return _entity.Dagdeel.Omschrijving; }
        }

        public string Datum
        {
            get { return _entity.Datum.GetDynamoDatum(); }
        }

        public string Taak
        {
            get { return _entity.Taak.Omschrijving; }
        }
    }
}