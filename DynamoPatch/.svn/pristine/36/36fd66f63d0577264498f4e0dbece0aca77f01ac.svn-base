using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Dynamo.BL;
using Dynamo.Model.Context;

namespace Dynamo.Test.BL.Repository
{
    [TestFixture]
    class BeheerderRepositoryTest
    {
        [Test]
        public void Test1()
        {
            var context = new FakeDynamoContext();
            FakeDynamoContext.SetBasisgegevens(context);
            context.Beheerders.Add(new Model.Beheerder { Id = 2, Naam = "Test" });
            using (var repo = new BeheerderRepository(context))
            {
                Assert.AreEqual(repo.CurrentBeheerder.Naam, "Herman");
                Assert.IsTrue(repo.IsBeheerderIngelogdVoorDagDeel(1));
                Assert.IsFalse(repo.IsBeheerderIngelogdVoorDagDeel(2));
                Assert.IsTrue(repo.Load().Count == 1);
                Assert.IsTrue(repo.Load(1).Naam == "Herman");
                Assert.IsTrue(repo.Load(2).Naam == "Test");
                repo.AdminBeheerder = true;
                Assert.IsTrue(repo.CurrentBeheerder.Naam == "Herman");
            }
        }

        [Test]
        public void BeheerderRepositoryTest_Test_AantalBerichten()
        { 
            var context = new FakeDynamoContext();
            FakeDynamoContext.SetBasisgegevens(context);
            context.Beheerders.Add(new Model.Beheerder { Id = 2, Naam = "Test" });
            using (var repo = new BeheerderRepository(context))
            {
                //Act
                var aantal = repo.GetAantalNieuweBerichten();

                //Assert
                Assert.AreEqual(aantal, 0);
            }
        }

        [Test]
        public void BeheerderRepositoryTest_Load()
        {
            var context = new FakeDynamoContext();
            FakeDynamoContext.SetBasisgegevens(context);
            
            using (var repo = new BeheerderRepository(context))
            {
                //Act
                var beheerders = repo.Load(beheerder=>!beheerder.Verwijderd);

                //Assert
                Assert.AreEqual(beheerders.Count, 1);
            }
        }
    }
}
