using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Dynamo.Model.Context;
using Dynamo.BL;

namespace Dynamo.Test.BL.Repository
{
    [TestFixture]
    class BoekingRepositoryTest
    {
        [Test]
        public void BoekingRepositoryTest_Test()
        {
            //Arrange
            var context = new FakeDynamoContext();
            FakeDynamoContext.GetInstance().Boekingen.Add(new Model.Boeking
            {
                DatumGeboekt = DateTime.Today
            });

            using (var repo = new BoekingRepository())
            {
                //Act
                var boekingen = repo.Load(boeking => boeking.DatumGeboekt == DateTime.Today);

                //Assert
                Assert.IsNotNull(boekingen);
                Assert.AreNotEqual(boekingen.Count, 0);
            }
        
        }

    }
}
