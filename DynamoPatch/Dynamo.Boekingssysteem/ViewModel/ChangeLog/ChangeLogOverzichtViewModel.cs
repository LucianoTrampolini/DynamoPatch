using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dynamo.BoekingsSysteem.Base;
using System.Collections.ObjectModel;
using Dynamo.BL;
using Dynamo.Common.Properties;

namespace Dynamo.Boekingssysteem.ViewModel.ChangeLog
{
    public class ChangeLogOverzichtViewModel : WorkspaceViewModel
    {
        public ObservableCollection<ChangeLogViewModel> ChangeLog
        {
            get 
            {
                using (var repo = new ChangeLogRepository())
                {
                    List<ChangeLogViewModel> all =
                                (from cust in repo.Load()
                                 select new ChangeLogViewModel(cust)).ToList();
                    return new ObservableCollection<ChangeLogViewModel>(all);
                }
            }
        }

        public ChangeLogOverzichtViewModel()
        {
            DisplayName = StringResources.ButtonChangeLog;
        }
    }
}
