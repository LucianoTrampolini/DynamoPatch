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
    public class BijwerkenPlanningContractTest
    {
        [Test]
        public void BijwerkenPlanningContractTest_Succes()
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
                        BeginContract = DateTime.Today.AddDays(-7),
                        OefenruimteId=1,
                        DagdeelId=2,
                        Oefendag=3
                    }
                }
            };
            FakeDynamoContext.SetBasisgegevens(context);
            using (var br = new BijwerkenPlanningContract(context))
            {
                //Act
                br.Execute(band);

                //Assert
                Assert.AreEqual(context.Planning.First().Boekingen.First().BandNaam, "Pipo");
                Assert.IsFalse(context.Planning.Any(p => p.Boekingen.Count() > 1));
                Assert.AreEqual(context.PlanningsDagen.Count(), context.Planning.Count());
                Assert.AreEqual(context.PlanningsDagen.Count(), context.Boekingen.Count());
                Assert.IsTrue(context.Planning.First().Datum.DagVanDeWeek() == 3);
            }
        }
    }
}
