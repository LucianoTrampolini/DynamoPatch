using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Dynamo.BL.Base;
using Dynamo.Model.Context;

namespace Dynamo.Test.Base
{
    [TestFixture]
    class BusinessRuleBaseTest
    {
        //Arrange
        private class testBase : BusinessRuleBase<Model.Beheerder>
        {
            public testBase()
                :base(new FakeDynamoContext())
            { }
        }

        [Test]
        [ExpectedException(typeof(NotImplementedException))]
        public void BusinessRuleBaseTest_Test()
        {
            using (var basetest = new testBase())
            {
                try
                {
                    basetest.Execute(new Model.Beheerder());
                }
                finally
                { 

                }
            }
        }
    }
}
