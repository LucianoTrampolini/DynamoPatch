using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Dynamo.Model.Context;
using Dynamo.BL.BusinessRules.Band;

namespace Dynamo.Test.BL.BusinessRules.Band
{
    [TestFixture]
    class BijwerkenContractTest
    {
        [Test]
        public void BijwerkenContract_Test()
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
                        Id=1,
                        BeginContract = DateTime.Today.AddMonths(-1),
                        OefenruimteId=1,
                        DagdeelId=2,
                        Oefendag=3,
                        MaandHuur=100
                    },
                    new Model.Contract
                    {
                        Id=0,
                        BeginContract = DateTime.Today,
                        OefenruimteId=1,
                        DagdeelId=3,
                        Oefendag=3,
                        MaandHuur=100
                    }
                },

            };
            FakeDynamoContext.SetBasisgegevens(context);

            using (var br = new BijwerkenContract(context))
            {
                //Act
                br.Execute(band);

                //Assert
                Assert.AreEqual(band.Contracten.Count, 2);
                Assert.IsTrue(band.Contracten.First().EindeContract.HasValue);
                Assert.AreEqual(band.Contracten.First().EindeContract.Value.AddDays(1), band.Contracten.Last().BeginContract);
            }
        }
    }
}
