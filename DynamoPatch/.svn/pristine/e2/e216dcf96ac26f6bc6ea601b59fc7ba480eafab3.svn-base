using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Dynamo.Model.Context;
using Dynamo.BL;
using Dynamo.Common;

namespace Dynamo.Test.BL.Repository
{
    [TestFixture]
    class BandRepositoryTest
    {
        [Test]
        public void BandRepositoryTest_GetOpenstaandeBedragenVoorDagdeel()
        {
            //Arrange
            var context = new FakeDynamoContext();
            context.Planning.Add(new Model.Planning
            {
                Datum = DateTime.Today,
                DagdeelId = 2,
                Boekingen = new List<Model.Boeking>
                {
                    new Model.Boeking
                    {
                        Band = new Model.Band 
                        { 
                            Id = 1, 
                            Naam = "Test" ,
                            Betalingen=new List<Model.Betaling>
                            {
                                new Model.Betaling { BandId = 1, Bedrag = 666 }
                            }
                        
                        }
                    },
                    new Model.Boeking
                    {
                        BandId = 2
                    },
                }
            });
            using (var repo = new BandRepository(context))
            {
                //Act
                var betalingen = repo.GetOpenstaandeBedragenVoorDagdeel(2);

                //assert
                Assert.NotNull(betalingen);
                Assert.AreEqual(betalingen.Count, 1);
                Assert.AreEqual(betalingen.First().BandNaam, "Test");
                Assert.AreEqual(betalingen.First().Bedrag, 666);
            }
        }
    }
}
