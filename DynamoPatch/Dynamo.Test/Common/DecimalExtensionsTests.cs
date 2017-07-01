using Dynamo.Common;
using NUnit.Framework;

namespace Dynamo.Test.Common
{
    [TestFixture]
    class DecimalExtensionsTests
    {
        [Test]
        public void GetDynamoBedragTest_Succes()
        {
            Assert.AreEqual((20.34m).GetDynamoBedrag(), "€ 20,34");
        }

        [Test]
        public void GetDynamoBedragTest_NoSucces()
        {
            Assert.AreNotEqual((20.34m).GetDynamoBedrag(), "$ 20,34");
        }
    }
}
