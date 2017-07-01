using System;
using System.Linq;

using Dynamo.BL.Base;
using Dynamo.BL.Repository;
using Dynamo.Common;
using Dynamo.Model;

namespace Dynamo.BL.BusinessRules.Band
{
    public class BijwerkenBetalingenContract : BusinessRuleBase<Model.Band>
    {
        #region Member fields

        private readonly BetalingRepository _bandRepository;

        #endregion

        public BijwerkenBetalingenContract(IDynamoContext context)
            : base(context)
        {
            _bandRepository = new BetalingRepository(context);
            _bandRepository.AdminModus = true;
        }

        public override bool Execute(Model.Band entity)
        {
            var returnvalue = true;
            var betalingen =
                _bandRepository.Load(
                    b =>
                        b.Datum.Month == DateTime.Today.Month && b.Datum.Year == DateTime.Today.Year
                            && b.BandId == entity.Id);

            if (betalingen.Count == 0)
            {
                foreach (
                    var contract in
                        entity.Contracten.Where(
                            c =>
                                c.MaandHuur != 0 && c.BeginContract <= DateTime.Today
                                    && (c.EindeContract.HasValue == false || c.EindeContract.Value >= DateTime.Today)))
                {
                    var betaling = new Betaling
                    {
                        Datum = DateTime.Today,
                        Bedrag = contract.MaandHuur,
                        BandId = entity.Id,
                        Opmerking = DateTime.Today.MaandVoluit()
                    };

                    _bandRepository.Save(betaling);
                }
            }
            return returnvalue;
        }

        public override void OnDispose()
        {
            _bandRepository.Dispose();
        }
    }
}