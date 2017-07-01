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
    class BijwerkenPlanningEindDatumContractTest
    {
        [Test]
        public void BijwerkenPlanningEindDatumContractTest_Succes()
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
                        EindeContract = DateTime.Today
                    }
                }
            };

            var datum = DateTime.Today.AddDays(14);
            while(datum.DagVanDeWeek() != 3)
            {
                datum=datum.AddDays(1);
            }

            FakeDynamoContext.SetBasisgegevens(context);
            context.Planning.Add
                (
                    new Model.Planning
                    {
                        OefenruimteId = 1,
                        DagdeelId = 2,
                        Datum = datum,
                        Boekingen = new List<Model.Boeking> 
                        { 
                            new Model.Boeking
                            {
                                BandId=1,
                                BandNaam = "Pipo",
                            }
                        }
                    }
                );
            using (var br = new BijwerkenPlanningEindDatumContract(context))
            {
                //Act
                br.Execute(band);

                //Assert
                Assert.AreEqual(context.Planning.First().Boekingen.Count(), 1);
                Assert.IsTrue(context.Planning.First().Boekingen.Any(b => b.Verwijderd));
            }
        }
    }
}
