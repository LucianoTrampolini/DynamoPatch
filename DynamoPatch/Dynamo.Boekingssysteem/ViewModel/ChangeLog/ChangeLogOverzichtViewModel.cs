using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Dynamo.BL;
using Dynamo.BoekingsSysteem.Base;
using Dynamo.Common.Properties;

namespace Dynamo.Boekingssysteem.ViewModel.ChangeLog
{
    public class ChangeLogOverzichtViewModel : WorkspaceViewModel
    {
        public ChangeLogOverzichtViewModel()
        {
            DisplayName = StringResources.ButtonChangeLog;
        }

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
    }
}