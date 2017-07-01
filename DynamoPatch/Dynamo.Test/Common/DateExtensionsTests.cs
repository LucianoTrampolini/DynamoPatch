using System;
using Dynamo.Common;
using NUnit.Framework;

namespace Dynamo.Test.Common
{
    [TestFixture]
    class DateExtensionsTests
    {
        [Test]
        public void DagVanDeWeekTest_Succes()
        {
           Assert.AreEqual(new DateTime(2012, 1, 30).DagVanDeWeek(), 1);
           Assert.AreEqual(new DateTime(2012, 1, 31).DagVanDeWeek(), 2);
           Assert.AreEqual(new DateTime(2012, 2, 1).DagVanDeWeek(), 3);
           Assert.AreEqual(new DateTime(2012, 2, 2).DagVanDeWeek(), 4);
           Assert.AreEqual(new DateTime(2012, 2, 3).DagVanDeWeek(), 5);
           Assert.AreEqual(new DateTime(2012, 2, 4).DagVanDeWeek(), 6);
           Assert.AreEqual(new DateTime(2012, 2, 5).DagVanDeWeek(), 7);
           Assert.AreEqual(new DateTime(2012, 2, 6).DagVanDeWeek(), 1);
        }

        [Test]
        public void DagVanDeWeekVoluitTest_Succes()
        {
            Assert.AreEqual(new DateTime(2012, 1, 30).DagVanDeWeekVoluit(), "Maandag");
            Assert.AreEqual(new DateTime(2012, 1, 31).DagVanDeWeekVoluit(), "Dinsdag");
            Assert.AreEqual(new DateTime(2012, 2, 1).DagVanDeWeekVoluit(), "Woensdag");
            Assert.AreEqual(new DateTime(2012, 2, 2).DagVanDeWeekVoluit(), "Donderdag");
            Assert.AreEqual(new DateTime(2012, 2, 3).DagVanDeWeekVoluit(), "Vrijdag");
            Assert.AreEqual(new DateTime(2012, 2, 4).DagVanDeWeekVoluit(), "Zaterdag");
            Assert.AreEqual(new DateTime(2012, 2, 5).DagVanDeWeekVoluit(), "Zondag");
            Assert.AreEqual(new DateTime(2012, 2, 6).DagVanDeWeekVoluit(), "Maandag");
        }

        [Test]
        public void MaandVoluitTest_Succes()
        {
            Assert.AreEqual(new DateTime(2012, 1, 30).MaandVoluit(), "Januari");
            Assert.AreEqual(new DateTime(2012, 2, 13).MaandVoluit(), "Februari");
            Assert.AreEqual(new DateTime(2012, 3, 1).MaandVoluit(), "Maart");
            Assert.AreEqual(new DateTime(2012, 5, 2).MaandVoluit(), "Mei");
            Assert.AreEqual(new DateTime(2012, 4, 3).MaandVoluit(), "April");
            Assert.AreEqual(new DateTime(2012, 6, 4).MaandVoluit(), "Juni");
            Assert.AreEqual(new DateTime(2012, 7, 5).MaandVoluit(), "Juli");
            Assert.AreEqual(new DateTime(2012, 8, 5).MaandVoluit(), "Augustus");
            Assert.AreEqual(new DateTime(2012, 9, 5).MaandVoluit(), "September");
            Assert.AreEqual(new DateTime(2012, 10, 5).MaandVoluit(), "Oktober");
            Assert.AreEqual(new DateTime(2012, 11, 5).MaandVoluit(), "November");
            Assert.AreEqual(new DateTime(2012, 12, 5).MaandVoluit(), "December");
        }

        [Test]
        public void GetIsoWeekNrTest_Succes()
        {
            Assert.AreEqual(new DateTime(2011, 1, 1).GetIsoWeekNr(), 52);
            Assert.AreEqual(new DateTime(2012, 1, 1).GetIsoWeekNr(), 52);
            Assert.AreEqual(new DateTime(2013, 1, 1).GetIsoWeekNr(), 1);
            Assert.AreEqual(new DateTime(2014, 1, 1).GetIsoWeekNr(), 1);
        }

        [Test]
        public void GetDynamoDatumTest_Succes()
        {
            Assert.AreEqual(new DateTime(2011, 1, 1).GetDynamoDatum(), "01-01-2011");
            Assert.AreEqual(new DateTime(2012, 10, 1).GetDynamoDatum(), "01-10-2012");
        }

        [Test]
        public void GetDynamoDatumTijdTest_Succes()
        {
            Assert.AreEqual(new DateTime(2011, 1, 1, 9, 23, 19).GetDynamoDatumTijd(), "01-01-2011 9:23");
        }
    }
}
