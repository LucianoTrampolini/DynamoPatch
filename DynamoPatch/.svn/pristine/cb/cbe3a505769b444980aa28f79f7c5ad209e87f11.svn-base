using System;
using System.Collections.Generic;
using System.Linq;
using Dynamo.BL.BusinessRules.Band;
using Dynamo.Model.Context;
using NUnit.Framework;

namespace Dynamo.Test.BL.BusinessRules.Band
{
    [TestFixture]
    class HandleNaamChangedTest
    {
        [Test]
        public void HandleNaamChangedTest_Succes()
        {
            //Arrange
            var context = new FakeDynamoContext();
            var band = new Model.Band
            {
                Id = 1,
                Naam = "Pipo"
            };
            context.Planning.Add(new Model.Planning { Datum = DateTime.Today.AddDays(10), Boekingen = new List<Model.Boeking> { new Model.Boeking { BandId = 1, BandNaam = "Clown" } } });
            context.Planning.Add(new Model.Planning { Datum = DateTime.Today.AddDays(-10), Boekingen = new List<Model.Boeking> { new Model.Boeking { BandId = 1, BandNaam = "Clown" } } });
            
            using (var br = new HandleNaamChanged(context))
            {
                //Act
                br.Execute(band);

                //Assert
                Assert.AreEqual(context.Planning.First().Boekingen.First().BandNaam, "Pipo");
                Assert.AreEqual(context.Planning.Last().Boekingen.Last().BandNaam, "Clown");
            }
        }
    }
}
