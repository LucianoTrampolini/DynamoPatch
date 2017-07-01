using System;
using Dynamo.BL.BusinessRules.Beheerder;
using Dynamo.Model.Context;
using NUnit.Framework;

namespace Dynamo.Test.BL.BusinessRules.Beheerder
{
    [TestFixture]
    class KanAanmeldenTest
    {
        [Test]
        public void KanAanmeldenTest_Succes()
        {
            //Arrange
            var context = new FakeDynamoContext();

            var vergoeding = new Model.Vergoeding { Datum = DateTime.Today, DagdeelId = 3, Beheerder = new Model.Beheerder { Id=1} };

            context.Vergoedingen.Add(new Model.Vergoeding { Datum = DateTime.Today, BeheerderId = 1, DagdeelId = 2 });
            using (var br = new KanAanmelden(context))
            {
                //Act
                var returnValue = br.Execute(vergoeding);

                //Assert
                Assert.IsTrue(returnValue);
            }
        }

        [Test]
        public void KanAanmeldenTest_NoSucces()
        {
            //Arrange
            var context = new FakeDynamoContext();

            var vergoeding = new Model.Vergoeding { Datum = DateTime.Today, DagdeelId = 2, Beheerder = new Model.Beheerder { Id = 1 } };

            context.Vergoedingen.Add(new Model.Vergoeding { Datum = DateTime.Today, BeheerderId = 1, DagdeelId = 2 });
            using (var br = new KanAanmelden(context))
            {
                //Act
                var returnValue = br.Execute(vergoeding);

                //Assert
                Assert.IsFalse(returnValue);
            }
        }
    }
}
