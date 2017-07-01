using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Dynamo.Model.Context;
using Dynamo.BL.BusinessRules.Band;
using Dynamo.Common;

namespace Dynamo.Test.BL.BusinessRules.Band
{
    [TestFixture]
    public class BijwerkenBetalingenContractTest
    {
        [Test]
        public void BijwerkenBetalingenContract_Test()
        { 
            //Arrange
            var context = new FakeDynamoContext();
            var band = new Model.Band
            {
                Id = 1,
                Naam = "Pipo",
                Contracten = new List<Model.Contract> 
                { 
                    new Model.Contract
                    {
                        BeginContract = DateTime.Today.AddMonths(-1),
                        OefenruimteId=1,
                        DagdeelId=2,
                        Oefendag=3,
                        MaandHuur=100
                    }
                },
                
            };
            FakeDynamoContext.SetBasisgegevens(context);
            context.Betalingen.Add(new Model.Betaling
            {
                BandId=1,
                Bedrag = 12,
                Opmerking = DateTime.Today.MaandVoluit(),
                Datum = DateTime.Today.AddYears(-1)
            });

            using (var br = new BijwerkenBetalingenContract(context))
            {
                //Act
                br.Execute(band);

                //Assert
                Assert.AreEqual(context.Betalingen.Last().Bedrag, 100);
                Assert.IsTrue(context.SaveAangeroepen == 1);
                Assert.IsTrue(context.Betalingen.Count() == 2);
            }
        }
    }
}
