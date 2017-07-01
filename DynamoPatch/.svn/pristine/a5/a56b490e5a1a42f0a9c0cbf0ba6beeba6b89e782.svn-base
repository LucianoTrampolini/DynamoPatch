using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Dynamo.BoekingsSysteem.ViewModel;
using Dynamo.BoekingsSysteem.Base;
using Dynamo.Boekingssysteem.ViewModel.Planning;
using Dynamo.Model.Context;
using System.Data.Entity;
using System.Data.Objects;

namespace Dynamo.Test.Boekingssysteem.ViewModel
{
    [TestFixture]
    class HoofdschermViewModelTests
    {
        [Test]
        public void HoofdSchermViewModelTest()
        {
            //Arrange
            HoofdSchermViewModel target =
                new HoofdSchermViewModel();

            //Test of het planningoverzicht geladen is
            Assert.IsTrue(target.Workspaces.Count == 1);
            var planningOverzicht = target.Workspaces[0] as PlanningOverichtViewModel;
            Assert.IsNotNull(planningOverzicht, "verkeerde default viewmodel geladen");

            // Find the command that opens the "All Customers" workspace.
            CommandViewModel commandVM =
                target.Commands.First(cvm => cvm.DisplayName == "Beheerders");

            // Open the "All Customers" workspace.
            commandVM.Command.Execute(null);
            Assert.AreEqual(2, target.Workspaces.Count, "Did not create viewmodel.");

            // sluit het planningsoverzicht workspace to close.
            planningOverzicht.CloseCommand.Execute(null);
           Assert.AreEqual(1, target.Workspaces.Count, "Did not close viewmodel.");
        }
    }
}
