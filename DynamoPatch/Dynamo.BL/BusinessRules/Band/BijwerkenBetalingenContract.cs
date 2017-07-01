using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dynamo.BL.Base;
using Dynamo.Common;
using Dynamo.Model;
using Dynamo.BL;
using Dynamo.BL.Repository;

namespace Dynamo.BL.BusinessRules.Band
{
    public class BijwerkenBetalingenContract : BusinessRuleBase<Model.Band>
    {
        private BetalingRepository _bandRepository = null;

        public BijwerkenBetalingenContract(IDynamoContext context)
            :base(context) 
        {
            _bandRepository = new BetalingRepository(context);
            _bandRepository.AdminModus = true;
        }

        public override bool Execute(Model.Band entity)
        {
            var returnvalue = true;
            var betalingen = _bandRepository.Load(b => b.Datum.Month == DateTime.Today.Month && b.Datum.Year == DateTime.Today.Year && b.BandId==entity.Id);

            if (betalingen.Count == 0)
            {
                foreach (var contract in entity.Contracten.Where(c => c.MaandHuur !=0 && c.BeginContract <= DateTime.Today && (c.EindeContract.HasValue == false || c.EindeContract.Value >= DateTime.Today)))
                {
                    var betaling = new Model.Betaling
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
