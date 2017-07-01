using System;
using Dynamo.Common;
using Dynamo.Common.Properties;
using NUnit.Framework;

namespace Dynamo.Test.Common
{
    [TestFixture]
    class CommonMethodsTests
    {
        [Test]
        public void DynamoDatum2DatumTest_Succes()
        {
            Assert.AreEqual(CommonMethods.DynamoDatum2Datum("1-2-2012"), new DateTime(2012, 2, 1));
        }

        [Test]
        public void DynamoDatum2DatumTest_NoSucces()
        {
            Assert.AreNotEqual(CommonMethods.DynamoDatum2Datum("1-2-2012"), new DateTime(2012, 1, 2));
        }

        [Test]
        public void IsDynamoDatumTest_Succes()
        {
            Assert.IsTrue(CommonMethods.IsDynamoDatum("1-2-2012"));
        }

        [Test]
        public void IsDynamoDatumTest_NoSucces()
        {
            Assert.IsFalse(CommonMethods.IsDynamoDatum("1-22-2012"));
        }

        [Test]
        public void GetBedragTest_Succes()
        {
            Assert.AreEqual(CommonMethods.GetBedrag(20.34m), "€ 20,34");
        }

        [Test]
        public void GetBedragTest_NoSucces()
        {
            Assert.AreNotEqual(CommonMethods.GetBedrag(20.34m), "$ 20,34");
        }

        [Test]
        public void GetOefendagenListTest_Succes()
        {
            var list = CommonMethods.GetOefenDagenList();

            Assert.IsTrue(list.Count == 7);
            Assert.IsTrue(list[0].Value == StringResources.DagMaandag);
            Assert.IsTrue(list[0].Key == 1);
            Assert.IsTrue(list[6].Value == StringResources.DagZondag);
            Assert.IsTrue(list[6].Key == 7);
            Assert.IsTrue(list[4].Value == StringResources.DagVrijdag);
            Assert.IsTrue(list[4].Key == 5);
        }
    }
}
